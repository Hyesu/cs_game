namespace HEngine;

public static class HPath
{
    public static string FindFilePathByRecursively(string srcPath, string fileName)
    {
        var dirs = srcPath.Split(Path.DirectorySeparatorChar);
        var dir = dirs.First(); // drive
        for (int i = 1; i < dirs.Length; i++)
        {
            dir += $"/{dirs[i]}";

            var fullPath = Path.Combine(dir, fileName);
            if (File.Exists(fullPath))
            {
                return fullPath;
            }
        }

        return string.Empty;
    }
}