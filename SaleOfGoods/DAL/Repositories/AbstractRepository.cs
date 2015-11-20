using Model;

namespace DAL.Repositories
{
    public class AbstractRepository
    {
        protected SalesDBEntities Context { get; private set; }

        protected AbstractRepository()
        {
            Context = new SalesDBEntities();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
