using EF.Data;

namespace EF.Providers.Abstract
{
    public abstract class BaseProvider
    {
        protected ContactsBookDbContext GetNewContext()
        {
            return new ContactsBookDbContext();
        }
    }
}