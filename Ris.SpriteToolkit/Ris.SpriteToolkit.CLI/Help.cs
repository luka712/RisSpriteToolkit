
using System.Text;

namespace Ris.SpriteToolkit.CLI;

/// <summary>
/// The help message for the CLI.
/// </summary>
internal static class Help
{
    /// <summary>
    /// Print the help message to the console.
    /// </summary>
    internal static void PrintHelp()
    {
        StringBuilder stringBuilder = new();
        stringBuilder.AppendLine("ris-asset-toolkit - A command line interface for 'RisSpriteToolkit'.");
        stringBuilder.AppendLine();
        stringBuilder.AppendLine("Usage:");
        stringBuilder.AppendLine("  ris-asset-toolkit -d <directory> [options]");
        stringBuilder.AppendLine();
        stringBuilder.AppendLine("Options:");
        stringBuilder.AppendLine("  -o, --output <directory>      Specify the output directory. Defaults to the current directory.");
        stringBuilder.AppendLine("  -d, --directory <directory>   Specify the directory containing images to process.");
        stringBuilder.AppendLine("  -s, --spritesheet <name>      Specify the name of the sprite sheet. The '_{sprite_sheet_index}' is suffixed to the name. Defaults to 'sprite_sheet'.");
        stringBuilder.AppendLine("  -m, --meta <name>             Specify the name of the JSON metadata file. Defaults to 'assets'.");
        stringBuilder.AppendLine("  -r, --replace <yes/no>        Specify whether to replace existing files. Defaults to 'no'.");
        stringBuilder.AppendLine("  -p, --padding <pixels>        Specify the padding (in pixels) between sprites in the sprite sheet. Defaults to 1.");
        stringBuilder.AppendLine("  -h, --help                    Show this help message and exit.");
        Console.WriteLine(stringBuilder.ToString());
    }
}
