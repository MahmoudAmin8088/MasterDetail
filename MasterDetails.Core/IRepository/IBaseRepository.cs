using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDetails.Core.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        ICollection<T> GetAll();
        T GetById(int id);
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);

    }
}
