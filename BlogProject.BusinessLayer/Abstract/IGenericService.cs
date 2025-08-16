using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.BusinessLayer.Abstract
{
    public interface IGenericService<T>
    {
        List<T> GetAllBL();
        T GetByIdBL(int id);
        void InsertBL(T entity);
        void UpdateBL(T entity);
        void DeleteBL(T entity);
        List<T> List(Expression<Func<T, bool>> filter);
    }
}
