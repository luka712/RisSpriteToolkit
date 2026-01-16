using OpenCvSharp.ImgHash;
using System.Drawing;
using System.Xml.XPath;

namespace RisGameFramework.SpriteToolkit;

/// <summary>
/// The sprite sheet which uses the Skyline packing algorithm.
/// </summary>
internal class BuilderSkylineSpriteSheet : ABaseBuilderSpriteSheet
{
    private readonly List<BuilderSkylineNode> skyline = new();

    /// <summary>
    /// The constructor for <see cref="BuilderSkylineSpriteSheet"/>.
    /// </summary>
    /// <param name="name">The name of sprite sheet.</param>
    /// <param name="size">The size of sprite sheet.</param>
    internal BuilderSkylineSpriteSheet(string name = "SpriteSheet", Size? size = null)
        : base(name, size)
    {
    }

    /// <inheritdoc/>
    public override bool FitsInSheet(RawImage image, out int pixelX, out int pixelY)
    {
        return FindPosition(
            image.Width,
            image.Height,
            out pixelX,
            out pixelY,
            out int bestIndex);
    }

    /// <inheritdoc/>
    public override BuilderSprite AddSprite(RawImage image)
    {
        if (image.Width > Size.Width || image.Height > Size.Height)
        {
            throw new ArgumentException($"Image size {image.Width}x{image.Height} exceeds available sprite sheet size {Size.Width}x{Size.Height}.");
        }

        int nodeWidth = image.Width + Padding * 2;
        int nodeHeight = image.Height + Padding * 2;

        // If skyline is empty, initialize it.
        if (skyline.Count == 0)
        {
            // Initialize the skyline with a single node spanning the width of the sheet
            skyline.Add(new BuilderSkylineNode(0, 0, Size.Width));
        }


        if (FindPosition(nodeWidth, nodeHeight, out int x, out int y, out int bestIndex))
        {
            // Add the skyline level for the new sprite
            AddSkylineLevel(bestIndex, x, y, nodeWidth, nodeHeight);

            // Create and add the sprite
            x += Padding;
            y += Padding;

            BuilderSprite sprite = new BuilderSprite(image, new Point(x, y), this);
            sprites.Add(sprite);
            return sprite;
        }

        throw new InvalidOperationException("No space available to add the sprite.");
    }

    /// <inheritdoc/>
    public override bool TryAddSprite(RawImage image, out BuilderSprite? sprite)
    {
        int nodeWidth = image.Width + Padding * 2;
        int nodeHeight = image.Height + Padding * 2;

        if (FindPosition(nodeWidth, nodeHeight, out int x, out int y, out int bestIndex))
        {
            AddSkylineLevel(bestIndex, x, y, nodeWidth, nodeHeight);

            // Create and add the sprite
            sprite = new BuilderSprite(image, new Point(x + Padding, y + Padding), this);
            sprites.Add(sprite);
            return true;
        }
        sprite = null;
        return false;
    }

    /// <summary>
    /// Checks if a rectangle of size (width, height) fits at the skyline node index.
    /// </summary>
    /// <param name="index">The index of a node.</param>
    /// <param name="width">The required width.</param>
    /// <param name="height">The required height.</param>
    /// <param name="outY">The position Y in a node.</param>
    /// <returns><c>true</c> if fits; <c>false</c> otherwise.</returns>
    private bool FitsToNode(int index, int width, int height, out int outY)
    {
        outY = -1;

        int x = skyline[index].X;
        int y = skyline[index].Y;

        int spaceWidth = width;

        // Scan horizontally along the skyline to ensure height clearance
        int i = index;
        int maxY = y;
        while (spaceWidth > 0)
        {
            // If we exceed the skyline nodes, it cannot fit
            if (i >= skyline.Count)
            {
                return false;
            }

            // Update the maximum Y encountered in this horizontal scan
            maxY = System.Math.Max(maxY, skyline[i].Y);
            if (maxY + height > Size.Height)
            {
                return false;
            }
            spaceWidth -= skyline[i].Width;
            i++;
        }

        outY = maxY;
        return true;
    }

    /// <summary>
    /// Finds the best position to fit a rectangle of size (w, h) in the skyline.
    /// </summary>
    /// <param name="w">The width required.</param>
    /// <param name="h">The height required.</param>
    /// <param name="bestX">The best X position. Ignore if <c>false</c> is returned.</param>
    /// <param name="bestY">The best Y position. Ignore if <c>false</c> is returned.</param>
    /// <param name="bestIndex">The best skyline index. Ignore if <c>false</c> is returned.</param>
    /// <returns>
    /// <c>true</c> if a position is found; otherwise, <c>false</c>.
    /// </returns>
    private bool FindPosition(int w, int h, out int bestX, out int bestY, out int bestIndex)
    {
        bestX = -1;
        bestY = Int32.MaxValue;
        bestIndex = -1;

        for (int i = 0; i < skyline.Count; i++)
        {
            if (FitsToNode(i, w, h, out int y))
            {
                if (y < bestY)
                {
                    bestY = y;
                    bestIndex = i;
                    bestX = skyline[i].X;
                }
            }
        }

        return bestIndex != -1;
    }

    /// <summary>
    /// Adds a new skyline level at the specified index.
    /// </summary>
    /// <param name="index">
    /// The index at which to add the new skyline level.
    /// </param>
    /// <param name="x">The x position of level.</param>
    /// <param name="y">The y position of level.</param>
    /// <param name="w">The width of level.</param>
    /// <param name="h">The height of level.</param>
    private void AddSkylineLevel(int index, int x, int y, int w, int h)
    {
        BuilderSkylineNode newNode = new BuilderSkylineNode(x, y + h, w);
        skyline.Insert(index, newNode);

        // Remove covered skyline parts
        int i = index + 1;
        while (i < skyline.Count)
        {
            if (skyline[i].X < x + w)
            {
                int shrink = (skyline[i].X + skyline[i].Width) - (x + w);
                if (shrink > 0)
                {
                    skyline[i].X = x + w;
                    skyline[i].Width = shrink;
                }
                else
                {
                    skyline.RemoveAt(i);
                }
            }
            else break;
        }

        // Merge adjacent nodes of equal height
        for (i = 0; i < skyline.Count - 1; i++)
        {
            if (skyline[i].Y == skyline[i + 1].Y)
            {
                skyline[i].Width += skyline[i + 1].Width;
                skyline.RemoveAt(i + 1);
                i--;
            }
        }
    }
}
