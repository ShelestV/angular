using ContactsBook.Data;

namespace ContactsBook.DbProviders.Abstract
{
    internal abstract class BaseProvider
    {
        protected readonly ContactsBookDbContext dbContext;

        protected BaseProvider(ContactsBookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}