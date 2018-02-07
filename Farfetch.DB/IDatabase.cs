using System;
using System.Threading.Tasks;

namespace Farfetch.DB
{
    public interface IDatabase<T>
    {
        void Init(DbSettings settings);

        T GetDatabase();
    }
}