using EF.Data;

namespace EF.Providers.Abstract
{
    public abstract class BaseProvider
    {
        protected readonly ContactsBookDbContext dbContext;

        protected BaseProvider()
        {
            dbContext = new ContactsBookDbContext();
        }
    }
}