
using Ris.SpriteToolkit;
using Ris.SpriteToolkit.CLI;


bool showHelp = HasArgument(args, "-h") || HasArgument(args, "--help") || args.Length <= 1;
if(showHelp)
{
    Help.PrintHelp();
    return;
}

string? inputDirectory = ArgHelper.GetArgumentValue(args, "-d") ?? ArgHelper.GetArgumentValue(args, "--data");
string outputDirectory = ArgHelper.GetArgumentValue(args, "-o") ?? ArgHelper.GetArgumentValue(args, "--output") ?? Directory.GetCurrentDirectory();
string spriteSheetName = ArgHelper.GetArgumentValue(args, "-s") ?? ArgHelper.GetArgumentValue(args, "--spritesheet") ?? "sprite_sheet";
string jsonName = ArgHelper.GetArgumentValue(args, "-m") ?? ArgHelper.GetArgumentValue(args, "--meta") ?? "assets";
string[] yesArgs = ["y", "yes", "true"];
bool replace = ArgHelper.ArgumentHasAnyValue(args, "-r", yesArgs) || ArgHelper.ArgumentHasAnyValue(args, "--replace", yesArgs);
int padding = ArgHelper.GetArgumentValue<int?>(args, "-p") ?? ArgHelper.GetArgumentValue<int?>(args, "--padding") ?? 1;

SpriteTKBundleBuilder bundleBuilder = new();
bundleBuilder.AllowReplace = replace;
bundleBuilder.PngSpriteSheetBuilder.AllowReplace = replace;
bundleBuilder.PngSpriteSheetBuilder.DefaultSheetName = spriteSheetName;
bundleBuilder.PngSpriteSheetBuilder.Padding = padding;

if (!String.IsNullOrEmpty(inputDirectory))
{
    bundleBuilder.AddDirectoryContents(inputDirectory, true);
}
try
{
    bundleBuilder.SaveBundle(outputDirectory, jsonName);
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

bool HasArgument(string[] allArguments, string arg)
{
    foreach (var argument in allArguments)
    {
        if (argument == arg)
        {
            return true;
        }
    }
    return false;
}



