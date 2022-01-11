namespace ConsoleLauncher
{
    using System;

    public class DisposableBase : IDisposable
    {

        #region IDisposable Functions

        private readonly object m_DisposeSyncRoot = new object();
        private bool m_IsDisposed;

        /// <summary>
        /// Finaliser function.
        /// </summary>
        ~DisposableBase()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets a value indicating whether this object has been disposed.
        /// </summary>
        protected bool IsDisposed
        {
            get { return m_IsDisposed; }
            private set { m_IsDisposed = value; }
        }

        /// <summary>
        /// Throws an exception if this object has been disposed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">Thrown if this object has been disposed.</exception>
        protected void CheckDisposed()
        {
            if (IsDisposed) throw new ObjectDisposedException("The object has been disposed.");
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Cleans up the object.
        /// </summary>
        /// <param name="disposing">If true then called by Dispose function.</param>
        protected virtual void Dispose(bool disposing)
        {
            lock (m_DisposeSyncRoot)
            {
                if (!IsDisposed)
                {
                    if (disposing)
                    {
                        // Free other state (managed objects).
                        DisposeManagedResources();
                    }

                    // Free your own state (unmanaged objects).
                    // Set large fields to null.
                    DisposeUnmanagedResources();

                    IsDisposed = true;
                }
            }
        }

        /// <summary>
        /// Dispose managed resources.
        /// </summary>
        protected virtual void DisposeManagedResources()
        {
        }

        /// <summary>
        /// Disposes the unmanaged resources.
        /// </summary>
        protected virtual void DisposeUnmanagedResources()
        {
        }

        #endregion

    }
}
