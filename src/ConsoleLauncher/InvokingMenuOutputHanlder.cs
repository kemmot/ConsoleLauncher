namespace ConsoleLauncher
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public class InvokingMenuOutputHanlder : MenuOutputHandlerBase
    {
        public InvokingMenuOutputHanlder(IConsole console)
            : base(console)
        {
        }

        public int MaxItems { get; } = 5;

        public override void Handle(IReadOnlyCollection<MenuItem> items)
        {
            if (items.Count > this.MaxItems)
            {
                this.Output.WriteLine($"Asked to handle {items.Count} items (max is {this.MaxItems}), is this correct?");
                return;
            }

            base.Handle(items);
        }

        public override void Handle(MenuItem item)
        {
            var info = new ProcessStartInfo(item.Path);
            info.UseShellExecute = true;
            Process.Start(info);
        }
    }
}
