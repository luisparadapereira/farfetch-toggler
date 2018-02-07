namespace Farfetch.DB
{
    public interface IDatabase<T>
    {
        /// <summary>
        /// Initializes the database
        /// </summary>
        /// <param name="settings">Database connection settings</param>
        void Init(DbSettings settings);

        /// <summary>
        /// Retrieves the database
        /// </summary>
        /// <returns>The database type</returns>
        T GetDatabase();

        /// <summary>
        /// Builds the connection string based on the setting
        /// </summary>
        void BuildConnectionString();
    }
}