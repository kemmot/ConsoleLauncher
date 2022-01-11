namespace ConsoleLauncher
{
    using System;

    public class SystemConsole2 : SystemConsole, IConsole2
    {
        public IDisposable PushBackgroundColor(ConsoleColor newValue)
        {
            var originalValue = this.BackgroundColor;
            this.BackgroundColor = newValue;
            return new PropertyContext<ConsoleColor>(this, originalValue, (target, localOriginalValue) => { target.BackgroundColor = localOriginalValue; });
        }

        public IDisposable PushForegroundColor(ConsoleColor newValue)
        {
            var originalValue = this.ForegroundColor;
            this.ForegroundColor = newValue;
            return new PropertyContext<ConsoleColor>(this, originalValue, (target, localOriginalValue) => { target.ForegroundColor = localOriginalValue; });
        }

        class PropertyContext<T> : DisposableBase
        {
            private readonly SystemConsole m_Console;
            private readonly T m_OriginalValue;
            private readonly Action<SystemConsole, T> m_Setter;

            public PropertyContext(SystemConsole console, T originalValue, Action<SystemConsole, T> setter)
            {
                m_Console = console ?? throw new ArgumentNullException(nameof(console));
                m_Setter = setter ?? throw new ArgumentNullException(nameof(setter));
                m_OriginalValue = originalValue;
            }

            protected override void DisposeManagedResources()
            {
                m_Setter(m_Console, m_OriginalValue);
                base.DisposeManagedResources();
            }
        }
    }
}
