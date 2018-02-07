namespace Farfetch.DB
{
    public abstract class BaseDatabase
    {
        internal abstract string BuildConnectionString(DbSettings settings);
    }
}