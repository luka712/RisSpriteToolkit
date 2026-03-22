using System.Drawing;

namespace RisGameFramework.SpriteToolkit.Exceptions
{
    /// <summary>
    /// Thrown when attempting to add a sprite to a full sprite sheet.
    /// </summary>
    public sealed class SpriteSheetFullException : Exception
    {
        /// <summary>
        /// The size of the sprite sheet.
        /// </summary>
        public Size SheetSize { get; }

        /// <summary>
        /// The size of the sprite that could not be added.
        /// </summary>
        public Size SpriteSize { get; }

        /// <summary>
        /// The constructor for <see cref="SpriteSheetFullException"/>.
        /// </summary>
        /// <param name="sheetSize">The size of the sprite sheet.</param>
        /// <param name="spriteSize">The size of the sprite that could not be added.</param>
        public SpriteSheetFullException(Size sheetSize, Size spriteSize)
            : base($"Sprite sheet is full. Cannot fit sprite of size {spriteSize.Width}x{spriteSize.Height}.")
        {
            SheetSize = sheetSize;
            SpriteSize = spriteSize;
        }
    }
}
