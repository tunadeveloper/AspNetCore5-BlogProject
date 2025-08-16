using BlogProject.DataAccessLayer.Abstract;
using BlogProject.DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly Context _context;

        public GenericRepository(Context context)
        {
            _context = context;
        }

        public void Delete(T entity)
        {
           _context.Remove(entity);
            _context.SaveChanges();
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
           return _context.Set<T>().Find(id);
        }

        public void Insert(T entity)
        {
          _context.Add(entity);
            _context.SaveChanges();
        }

        public List<T> List(Expression<Func<T, bool>> filter)
        {
         return _context.Set<T>().Where(filter).ToList();
        }

        public void Update(T entity)
        {
         _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
