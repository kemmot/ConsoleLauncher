namespace ConsoleLauncher
{
    using System;
    using System.Collections.Generic;
    
    public class Menu
    {
        public ConsoleColor? ResultBackgroundColor { get; set; }

        public ConsoleColor? ResultForegroundColor { get; set; }

        public ConsoleColor? AlternatingResultBackgroundColor { get; set; }

        public ConsoleColor? AlternatingResultForegroundColor { get; set; }

        public string Prompt { get; set; } = "Search string";

        public IReadOnlyList<MenuItem> ShowDialog(IMenuInput input, IConsole2 output)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (output == null) throw new ArgumentNullException(nameof(output));

            var context = new Context(output);

            context.CursorLeft = context.Output.CursorLeft;
            context.CursorTop = context.Output.CursorTop;
            context.ResultsTop = context.Output.CursorTop + 1;

            context.FilteredItemsChanged += Context_FilteredItemsChanged;
            context.SetInputItems(input.GetInputItems());
            context.SetFilteredItems(context.InputItems);

            do
            {
                context.Output.SetCursorPosition(context.CursorLeft, context.CursorTop);
                context.Output.Write(string.Format("{0}: {1}", Prompt, context.SearchStringRaw).PadRight(output.WindowWidth));
                ConsoleKeyInfo key = context.Output.ReadKey(true);
                context.Output.SetCursorPosition(context.CursorLeft, context.ResultsTop);
                HandleKey(key, context);
            }
            while (!context.Quit);

            // erase results line
            context.Output.SetCursorPosition(0, context.ResultsTop);
            EraseRemainingLine(context.Output);
            context.Output.SetCursorPosition(0, context.ResultsTop);

            return context.FilteredItems;
        }

        private void Context_FilteredItemsChanged(object sender, EventArgs e)
        {
            Context context = (Context)sender;
            Show(context);
        }

        private void HandleKey(ConsoleKeyInfo key, Context context)
        {
            switch (key.Key)
            {
                case ConsoleKey.Escape:
                    context.Quit = true;
                    context.ClearFilteredItems();
                    break;
                case ConsoleKey.Backspace:
                    if (context.SearchStringRaw.Length > 0)
                    {
                        context.SearchStringRaw = context.SearchStringRaw.Substring(0, context.SearchStringRaw.Length - 1);
                        context.Output.SetCursorPosition(0, context.ResultsTop);
                        Search(context);
                    }
                    break;
                case ConsoleKey.Enter:
                    context.Quit = true;
                    break;
                default:
                    if (key.KeyChar >= '1' && key.KeyChar <= '9')
                    {
                        int itemNumber = int.Parse(new string(key.KeyChar, 1));
                        int index = itemNumber - 1;
                        if (context.FilteredItems.Count > index)
                        {
                            var match = context.FilteredItems[index];
                            context.SetFilteredItem(match);
                            context.Quit = true;
                        }
                    }
                    else
                    {
                        context.SearchStringRaw += key.KeyChar;
                        context.Output.SetCursorPosition(0, context.ResultsTop);
                        Search(context);
                        Show(context.Output, context.FilteredItems, context.InputItems.Count);
                    }
                    break;
            }
        }

        private static void Search(Context context)
        {
            context.SetFilteredItems(Search(context.InputItems, context.SearchString));
        }

        private static List<MenuItem> Search(IReadOnlyList<MenuItem> inputOptions, string searchString)
        {
            var searchResults = new List<MenuItem>();
            foreach (var item in inputOptions)
            {
                if (item.Name.ToUpper().Contains(searchString))
                {
                    searchResults.Add(item);
                }
            }

            return searchResults;
        }

        private void Show(Context context)
        {
            context.Output.SetCursorPosition(0, context.ResultsTop);
            Show(context.Output, context.FilteredItems, context.InputItems.Count);
        }

        private void Show(IConsole2 output, IReadOnlyList<MenuItem> results, int totalCount)
        {
            output.CursorLeft = 0;
            int countLength = totalCount.ToString().Length;
            int baseLineLength = countLength * 2 + 3;

            output.Write($"{totalCount}/{totalCount}: ");

            int index = 0;
            bool quit = false;
            do
            {
                if (index >= results.Count)
                {
                    quit = true;
                }
                else
                {
                    string entry = results[index].Name;
                    string entryText = string.Empty;
                    if (index != 0)
                    {
                        entryText += ",";
                    }

                    entryText += $"{index + 1}:{entry}";
                    if (output.CursorLeft + entryText.Length >= output.WindowWidth)
                    {
                        quit = true;
                    }
                    else
                    {
                        GetColors(output, index, out ConsoleColor newBackgroundColor, out ConsoleColor newForegroundColor);
                        
                        using (output.PushBackgroundColor(newBackgroundColor))
                        using (output.PushForegroundColor(newForegroundColor))
                        {
                            output.Write(entryText);
                        }

                        index++;
                    }
                }
            }
            while (!quit);

            EraseRemainingLine(output);

            output.CursorLeft = 0;
            output.Write("{0}/{1}", index.ToString().PadLeft(countLength, ' '), results.Count.ToString().PadLeft(countLength, ' '));
        }

        private void GetColors(IConsole output, int index, out ConsoleColor backgroundColor, out ConsoleColor foregroundColor)
        {
            bool useAlternate = index % 2 == 0;
            ConsoleColor? newBackgroundColor = useAlternate ? AlternatingResultBackgroundColor : ResultBackgroundColor;
            ConsoleColor? newForegroundColor = useAlternate ? AlternatingResultForegroundColor : ResultForegroundColor;
            backgroundColor = newBackgroundColor.HasValue ? newBackgroundColor.Value : output.BackgroundColor;
            foregroundColor = newForegroundColor.HasValue ? newForegroundColor.Value : output.ForegroundColor;
        }

        private void EraseRemainingLine(IConsole output)
        {
            string remainingLineString = new string(' ', (output.WindowWidth - output.CursorLeft) - 1);
            output.Write(remainingLineString);
        }
    }
}
