namespace ConsoleLauncher
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class CacheMenuInput : IMenuInput
    {
        private List<MenuItem> m_CachedItems;

        private DateTime m_LastCacheRefresh = DateTime.MinValue;

        private readonly TimeSpan m_MaxAge;

        private readonly string m_Path;

        private readonly IMenuInput m_Source;

        public CacheMenuInput(IMenuInput source, string path, TimeSpan maxAge)
        {
            this.m_Source = source ?? throw new ArgumentNullException(nameof(source));

            if (path == null) throw new ArgumentNullException(nameof(path));
            if (path.Length == 0) throw new ArgumentException("Argument cannot be zero length", nameof(path));

            m_MaxAge = maxAge;
            m_Path = path;
        }

        public List<MenuItem> GetInputItems()
        {
            if (m_CachedItems == null)
            {
                LoadCache();
            }
            else if (DateTime.Now > m_LastCacheRefresh + m_MaxAge)
            {
                ForceCacheRefresh("cache expiry");
            }

            return m_CachedItems;
        }

        public void ForceCacheRefresh(string reason = "user request")
        {
            if (File.Exists(m_Path))
            {
                File.Delete(m_Path);
            }

            RefreshCache(reason);
        }

        private void LoadCache()
        {
            if (!File.Exists(m_Path))
            {
                RefreshCache("missing cache file");
            }

            var cachedItems = new List<MenuItem>();
            using (var reader = new StreamReader(m_Path))
            {
                string path;
                while ((path = reader.ReadLine()) != null)
                {
                    cachedItems.Add(new MenuItem(path));
                }
            }

            m_CachedItems = cachedItems;

            m_LastCacheRefresh = File.GetLastWriteTime(m_Path);
        }

        private void RefreshCache(string reason)
        {
            Console.WriteLine("Forcing cache refresh due to " + reason);
            m_CachedItems = m_Source.GetInputItems();

            using (var writer = new StreamWriter(m_Path))
            {
                foreach (var item in m_CachedItems)
                {
                    writer.WriteLine(item.Path);
                }
            }
        }
    }
}
