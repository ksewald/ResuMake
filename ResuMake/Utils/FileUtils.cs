using System;
using System.IO;
using System.Reflection;

namespace ResumMake
{
    public class FileUtils
    {
        public static string GetRootPath()
        {
            var executingAssembyFilePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath;

            if (!executingAssembyFilePath.Contains('/')) return null;

            return @executingAssembyFilePath.Substring(0, executingAssembyFilePath.LastIndexOf('/'));
        }

        public static string[] ReadFromDataList(string dataListFileName)
        {
            var root = GetRootPath();

            if (string.IsNullOrWhiteSpace(root)) return new string[0];

            var filePath = $"{root}/DataLists/{dataListFileName}";
            return File.ReadAllLines(filePath);
        }
    }
}
