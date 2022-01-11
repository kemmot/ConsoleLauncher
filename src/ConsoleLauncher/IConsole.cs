namespace ConsoleLauncher
{
    using System;

    public interface IConsole
    {
        ConsoleColor BackgroundColor { get; set; }

        int CursorLeft { get; set; }

        int CursorTop { get; set; }

        ConsoleColor ForegroundColor { get; set; }

        int WindowWidth { get; }

        void Clear();

        ConsoleKeyInfo ReadKey();

        ConsoleKeyInfo ReadKey(bool intercept);

        void SetCursorPosition(int left, int top);

        void Write(char value);

        void Write(string value);

        void Write(string format, params object[] args);

        void WriteLine();

        void WriteLine(string value);
    }
}
