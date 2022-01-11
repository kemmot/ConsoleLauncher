namespace ConsoleLauncher
{
    public class ConsoleWritelineMenuOutputHandler : MenuOutputHandlerBase
    {
        public ConsoleWritelineMenuOutputHandler(IConsole console)
            : base(console)
        {
        }

        public override void Handle(MenuItem item)
        {
            this.Output.WriteLine(item.Path);
        }
    }
}
