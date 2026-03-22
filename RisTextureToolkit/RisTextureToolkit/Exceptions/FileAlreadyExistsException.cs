namespace RisGameFramework.SpriteToolkit.Exceptions
{
    /// <summary>
    /// Thrown when attempting to create a file that already exists.
    /// </summary>
    public sealed class FileAlreadyExistsException : IOException
    {
        /// <summary>
        /// The file path that already exists.
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public FileAlreadyExistsException(string filePath)
            : base($"File already exists: {filePath}")
        {
            FilePath = filePath;
        }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="innerException">The inner exception.</param>
        public FileAlreadyExistsException(string filePath, Exception innerException)
            : base($"File already exists: {filePath}", innerException)
        {
            FilePath = filePath;
        }
    }
}
