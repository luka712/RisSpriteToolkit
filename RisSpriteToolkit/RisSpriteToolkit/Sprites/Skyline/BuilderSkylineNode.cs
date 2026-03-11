namespace RisSpriteToolkit.Sprites.Skyline
{
    /// <summary>
    /// The skyline node used in the skyline algorithm for sprite sheet packing.
    /// </summary>
    internal class BuilderSkylineNode
    {
        /// <summary>
        /// The constructor for <see cref="BuilderSkylineNode"/>.
        /// </summary>
        /// <param name="X">The x position.</param>
        /// <param name="Y">The y position.</param>
        /// <param name="Width">The width of node.</param>
        public BuilderSkylineNode(int x, int y, int width)
        {
            X = x;
            Y = y;
            Width = width;
        }

        /// <summary>
        /// The X position of this skyline node.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The Y position of this skyline node.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// The width of this skyline node.
        /// </summary>
        public int Width { get; set; }
    };
}

