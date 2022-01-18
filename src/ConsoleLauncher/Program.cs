namespace ConsoleLauncher
{
    using System;
    using System.Diagnostics;

    class Program
    {
        public ConsoleColor ResultForegroundColor = ConsoleColor.Green;
        public ConsoleColor ResultBackgroundColor = ConsoleColor.Black;
        public ConsoleColor AlternatingResultForegroundColor = ConsoleColor.Green;
        public ConsoleColor AlternatingResultBackgroundColor = ConsoleColor.Black;

        static void Main(string[] args)
        {
            bool refreshCache;
            if (args.Length > 0 && args[0].ToUpperInvariant() == "--REFRESHCACHE")
            {
                refreshCache = true;
            }
            else
            {
                refreshCache = false;
            }

            var output = new SystemConsole2();
            var menu = InitialiseMenu();
            var handler = InitialiseHandler(output);
            var input = InitialiseInput(refreshCache);

            handler.Handle(menu.ShowDialog(input, output));

            if (Debugger.IsAttached)
            {
                output.WriteLine("Press any key to quit");
                output.ReadKey();
            }
        }

        private static IMenuInput InitialiseInput(bool refreshCache)
        {
            var fileSystemInput = new FileSystemMenuInput();
            fileSystemInput.Paths.AddRange(Properties.Settings.Default.Paths.Split(';'));
            fileSystemInput.SearchPatterns.AddRange(Properties.Settings.Default.SearchPatterns.Split(';'));

            IMenuInput input;
            if (Properties.Settings.Default.CacheEnable)
            {
                var cachedInput = new CacheMenuInput(fileSystemInput, Properties.Settings.Default.CachePath, Properties.Settings.Default.CacheMaxAge);
                if (refreshCache)
                {
                    cachedInput.ForceCacheRefresh();
                }

                input = cachedInput;
            }
            else
            {
                input = fileSystemInput;
            }

            return input;
        }

        private static Menu InitialiseMenu()
        {
            var menu = new Menu();
            menu.ResultBackgroundColor = ParseColourString(Properties.Settings.Default.ResultBackgroundColor);
            menu.ResultForegroundColor = ParseColourString(Properties.Settings.Default.ResultForegroundColor);
            menu.AlternatingResultBackgroundColor = ParseColourString(Properties.Settings.Default.AlternatingResultBackgroundColor);
            menu.AlternatingResultForegroundColor = ParseColourString(Properties.Settings.Default.AlternatingResultForegroundColor);
            return menu;
        }

        private static IMenuOutputHandler InitialiseHandler(SystemConsole output)
        {
            var handlers = new MultiMenuOutputHandler(output);
            foreach (string action in Properties.Settings.Default.Actions.Split(';'))
            {
                switch (action.ToUpperInvariant())
                {
                    case "INVOKE":
                        handlers.Handlers.Add(new InvokingMenuOutputHanlder(output));
                        break;
                    case "PRINT":
                        handlers.Handlers.Add(new ConsoleWritelineMenuOutputHandler(output));
                        break;
                    default:
                        throw new NotSupportedException($"Unsupported action type: [{action}]");
                }
            }

            return handlers;
        }

        private static ConsoleColor? ParseColourString(string colourString)
        {
            ConsoleColor? colour;
            if (string.IsNullOrEmpty(colourString))
            {
                colour = null;
            }
            else
            {
                colour = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), colourString);
            }

            return colour;
        }
    }
}
