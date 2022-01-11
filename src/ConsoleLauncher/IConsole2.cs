namespace ConsoleLauncher
{
    using System;

    public interface IConsole2 : IConsole
    {
        IDisposable PushBackgroundColor(ConsoleColor color);

        IDisposable PushForegroundColor(ConsoleColor color);
    }
}
