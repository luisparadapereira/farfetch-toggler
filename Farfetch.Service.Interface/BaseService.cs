using Farfetch.CoreUnitOfWork;
using Farfetch.Models;
using Farfetch.ServiceManager.Interfaces;

namespace Farfetch.ServiceManager
{
    public class BaseService<T>: IService where T : DbT
    {
        protected CoreUnit<T> CoreUnit;

        public BaseService()
        {
            InitDb();
        }

        /// <inheritdoc />
        public void InitDb()
        {
            CoreUnit = new CoreUnit<T>();
            CoreUnit.InitDatabase();
        }
    }
}