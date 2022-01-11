namespace ConsoleLauncher
{
    using System;
    using System.Collections.Generic;

    public class Context
    {
        public event EventHandler FilteredItemsChanged;

        private readonly List<MenuItem> m_FilteredItems = new List<MenuItem>();

        private readonly List<MenuItem> m_InputItems = new List<MenuItem>();

        private string m_SearchStringRaw;

        public Context(IConsole2 output)
        {
            this.Output = output ?? throw new ArgumentNullException(nameof(output));
        }

        public IConsole2 Output { get; }

        public int CursorLeft { get; set; }

        public int CursorTop { get; set; }

        public IReadOnlyList<MenuItem> FilteredItems { get { return m_FilteredItems; } }

        public IReadOnlyList<MenuItem> InputItems { get { return m_InputItems; } }

        public bool Quit { get; set; }

        public int ResultsTop { get; set; }

        public string SearchStringRaw
        {
            get
            {
                return m_SearchStringRaw;
            }
            set
            {
                m_SearchStringRaw = value;
                SearchString = m_SearchStringRaw.ToUpperInvariant();
            }
        }

        public string SearchString { get; private set; }

        public void ClearFilteredItems()
        {
            this.m_FilteredItems.Clear();
        }

        public void SetFilteredItem(MenuItem item)
        {
            this.SetFilteredItems(new List<MenuItem>() { item });
        }

        public void SetFilteredItems(IEnumerable<MenuItem> items)
        {
            this.m_FilteredItems.Clear();
            this.m_FilteredItems.AddRange(items);
            this.OnFilteredItemsChanged(new EventArgs());
        }

        public void SetInputItems(IEnumerable<MenuItem> items)
        {
            this.m_InputItems.Clear();
            this.m_InputItems.AddRange(items);
        }

        protected void OnFilteredItemsChanged(EventArgs e)
        {
            this.FilteredItemsChanged?.Invoke(this, e);
        }
    }
}
