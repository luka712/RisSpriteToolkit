using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ris.AssetToolkit.UIClient.Data;

/// <summary>
/// The data class for a folder.
/// </summary>
public class FolderData
{
    /// <summary>
    /// Creates a new instance of <see cref="FolderData"/>.
    /// </summary>
    /// <param name="folderPath">
    /// The path of the folder. If <c>null</c> folder path is set to empty string.
    /// </param>
    public FolderData(string? folderPath)
    {
        FolderPath = folderPath ?? String.Empty;

        // Get subfolders
        if (!string.IsNullOrEmpty(folderPath))
        {
            try
            {
                FolderItems.Clear();

                string[] directories = System.IO.Directory.GetDirectories(folderPath);
                foreach (string dir in directories)
                {
                    FolderItems.Add(new FolderData(System.IO.Path.GetFileName(dir) ?? dir));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accessing folder {folderPath}: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// The path of the folder.
    /// </summary>
    public string FolderPath { get; set; } = String.Empty;

    /// <summary>
    /// The subfolders in the folder.
    /// </summary>
    public ObservableCollection<FolderData> FolderItems { get; set; } = new();

    /// <summary>
    /// Indicates whether the folder has subfolder or subfolders.
    /// </summary>
    public bool HasSubFolders => FolderItems.Count > 0;
}
