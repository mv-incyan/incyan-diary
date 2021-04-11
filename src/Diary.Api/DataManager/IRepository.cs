using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Api.DataManager
{
    public interface IRepository<T>
    {
        T Get(int id);
        int InsertOrUpdate(T entity);
        void Delete(int id);
        IQueryable<T> GetAll();

    }
}
