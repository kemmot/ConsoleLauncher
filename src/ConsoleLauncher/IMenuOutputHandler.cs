namespace ConsoleLauncher
{
    using System.Collections.Generic;

    public interface IMenuOutputHandler
    {
        void Handle(IReadOnlyCollection<MenuItem> items);

        void Handle(MenuItem item);
    }
}
