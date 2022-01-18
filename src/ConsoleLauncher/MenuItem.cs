namespace ConsoleLauncher
{
    using System;

    public class MenuItem
    {
        public MenuItem(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (path.Length == 0) throw new ArgumentException("Argument cannot be zero length", nameof(path));

            this.Path = path;
            Name = System.IO.Path.GetFileName(this.Path);
        }

        public string Name { get; }

        public string Path { get; }
    }
}
