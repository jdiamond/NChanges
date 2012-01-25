using System.Collections.Generic;
using System.IO;
using System.Linq;
using NChanges.Core;

namespace NChanges.Tool
{
    public static class PathHelper
    {
        public static IEnumerable<string> ExpandPaths(IEnumerable<string> paths)
        {
            return paths.SelectMany(p => ExpandPath(p));
        }

        public static IEnumerable<string> ExpandPath(string path)
        {
            string searchPath = ".";
            string searchPattern = path;

            if (path.Contains(Path.DirectorySeparatorChar) ||
                path.Contains(Path.AltDirectorySeparatorChar))
            {
                searchPath = Path.GetDirectoryName(path);
                searchPattern = Path.GetFileName(path);
            }

            return Directory.GetFiles(searchPath, searchPattern);
        }

        public static string FormatPath(string output, AssemblyInfo assembly)
        {
            return output.Replace("%name%", assembly.Name)
                         .Replace("%version%", assembly.Version);
        }

        public static void EnsureFolderExists(string outputPath)
        {
            var folderName = Path.GetDirectoryName(outputPath);

            if (!string.IsNullOrEmpty(folderName) && !Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }
        }
    }
}