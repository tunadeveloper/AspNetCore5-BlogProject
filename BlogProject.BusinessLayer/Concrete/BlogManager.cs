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
    public class BlogManager : IBlogService
    {
        private readonly IBlogDal _blogDal;

        public BlogManager(IBlogDal blogDal)
        {
            _blogDal = blogDal;
        }

        public void DeleteBL(Blog entity)
        {
            _blogDal.Delete(entity);
        }

        public List<Blog> GetAllBL()
        {
            return _blogDal.GetAll();
        }

        public List<Blog> GetBlogListByWriterBL(int id)
        {
            return _blogDal.List(x => x.WriterId == id);
        }

        public Blog GetByIdBL(int id)
        {
            return _blogDal.GetById(id);
        }

        public Blog GetByIdWithCategoryBL(int id)
        {
            return _blogDal.GetListWithCategory()
                 .FirstOrDefault(b => b.BlogId == id);
        }

        public List<Blog> GetListWithCategoryBL()
        {
            return _blogDal.GetListWithCategory();
        }

        public void InsertBL(Blog entity)
        {
            _blogDal.Insert(entity);
        }

        public List<Blog> List(Expression<Func<Blog, bool>> filter)
        {
            return _blogDal.List(filter);
        }

        public void UpdateBL(Blog entity)
        {
           _blogDal.Update(entity);
        }
    }
}
