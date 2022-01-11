namespace ConsoleLauncher
{
    using System.Collections.Generic;

    public abstract class MenuOutputHandlerBase : IMenuOutputHandler
    {
        protected MenuOutputHandlerBase(IConsole console)
        {
            this.Output = console;
        }

        protected IConsole Output { get; }

        public virtual void Handle(IReadOnlyCollection<MenuItem> items)
        {
            foreach (var item in items)
            {
                this.Handle(item);
            }
        }

        public abstract void Handle(MenuItem item);
    }
}
