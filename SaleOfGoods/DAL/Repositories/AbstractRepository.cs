using System;
using Model;

namespace DAL.Repositories
{
    public class AbstractRepository : IDisposable
    {
        private bool _disposed;
        protected SalesDBEntities Context { get; private set; }

        protected AbstractRepository()
        {
            Context = new SalesDBEntities();
            _disposed = false;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            CleanUp(true);
            GC.SuppressFinalize(this);
        }

        ~AbstractRepository()
        {
            CleanUp(false);
        }

        private void CleanUp(bool clean)
        {
            if (!_disposed)
                if(clean)
                    Context.Dispose();
            _disposed = true;
        }
    }
}
