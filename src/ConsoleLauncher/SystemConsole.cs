namespace ConsoleLauncher
{
    using System;

    public class SystemConsole : IConsole
    {
        public ConsoleColor BackgroundColor
        {
            get { return Console.BackgroundColor; }
            set { Console.BackgroundColor = value; }
        }

        public int CursorLeft
        {
            get { return Console.CursorLeft; }
            set { Console.CursorLeft = value; }
        }

        public int CursorTop
        {
            get { return Console.CursorTop; }
            set { Console.CursorTop = value; }
        }

        public ConsoleColor ForegroundColor
        {
            get { return Console.ForegroundColor; }
            set { Console.ForegroundColor = value; }
        }

        public int WindowWidth
        {
            get { return Console.WindowWidth; }
        }

        public void Clear()
        {
            Console.Clear();
        }

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }

        public void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }

        public void Write(char value)
        {
            Console.Write(value);
        }

        public void Write(string value)
        {
            Console.Write(value);
        }

        public void Write(string format, params object[] args)
        {
            Console.Write(format, args);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }
    }
}
