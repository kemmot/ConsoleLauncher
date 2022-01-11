namespace ConsoleLauncher
{
    using System;

    public class MenuItem
    {
        public MenuItem(string name, string path)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (name.Length == 0) throw new ArgumentException("Argument cannot be zero length", nameof(name));

            if (path == null) throw new ArgumentNullException(nameof(path));
            if (path.Length == 0) throw new ArgumentException("Argument cannot be zero length", nameof(path));

            this.Name = name;
            this.Path = path;
        }

        public string Name { get; }

        public string Path { get; }
    }
}
