using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        int Add(T t);
        int Delete(int id);
        int Update(T t);
        T GetId(int id);
    }
}
