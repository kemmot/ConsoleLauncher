namespace ConsoleLauncher
{
    using System.Collections.Generic;
    using System.IO;

    public class FileSystemMenuInput : IMenuInput
    {
        public List<string> SearchPatterns { get; } = new List<string>();

        public List<string> Paths { get; } = new List<string>();

        public List<MenuItem> GetInputItems()
        {
            var allResults = new List<MenuItem>();
            foreach (string path in this.Paths)
            {
                foreach (string searchPattern in this.SearchPatterns)
                {
                    foreach (string file in Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories))
                    {
                        allResults.Add(new MenuItem(Path.GetFileName(file), file));
                    }
                }
            }

            return allResults;
        }
    }
}
