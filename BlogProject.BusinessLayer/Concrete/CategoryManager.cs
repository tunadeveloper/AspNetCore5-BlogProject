using BlogProject.BusinessLayer.Abstract;
using BlogProject.DataAccessLayer.Abstract;
using BlogProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public void DeleteBL(Category entity)
        {
           _categoryDal.Delete(entity);
        }

        public List<Category> GetAllBL()
        {
            return _categoryDal.GetAll();
        }

        public Category GetByIdBL(int id)
        {
           return _categoryDal.GetById(id);
        }

        public void InsertBL(Category entity)
        {
            _categoryDal.Insert(entity);
        }

        public List<Category> List(Expression<Func<Category, bool>> filter)
        {
          return _categoryDal.List(filter);
        }

        public void UpdateBL(Category entity)
        {
           _categoryDal.Update(entity);
        }
    }
}
