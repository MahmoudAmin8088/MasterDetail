using MasterDetails.Core.IRepository;
using MasterDetails.Ef.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterDetails.Ef.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public T Create(T entity)
        {   
            _context.Set<T>().Add(entity);
            return entity;

        }

        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return entity;
        }

        public ICollection<T> GetAll()
        {
            var result = _context.Set<T>().ToList();
            return result;
        }

        public T GetById(int id)
        {
            var result = _context.Set<T>().Find(id);
            return result;
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;  
        }
    }
}
