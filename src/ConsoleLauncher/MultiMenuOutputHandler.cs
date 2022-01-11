namespace ConsoleLauncher
{
    using System.Collections.Generic;

    public class MultiMenuOutputHandler : MenuOutputHandlerBase
    {
        public MultiMenuOutputHandler(IConsole console)
            : base(console)
        {
        }

        public List<IMenuOutputHandler> Handlers { get; } = new List<IMenuOutputHandler>();

        public override void Handle(IReadOnlyCollection<MenuItem> items)
        {
            foreach (var handler in Handlers)
            {
                handler.Handle(items);
            }
        }

        public override void Handle(MenuItem item)
        {
            foreach (var handler in Handlers)
            {
                handler.Handle(item);
            }
        }
    }
}
