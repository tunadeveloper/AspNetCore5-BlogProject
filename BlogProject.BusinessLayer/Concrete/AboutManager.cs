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
    public class AboutManager : IAboutService
    {
        private readonly IAboutDal _aboutDal;

        public AboutManager(IAboutDal aboutDal)
        {
            _aboutDal = aboutDal;
        }

        public void DeleteBL(About entity)
        {
            _aboutDal.Delete(entity);
        }

        public List<About> GetAllBL()
        {
           return _aboutDal.GetAll();
        }

        public About GetByIdBL(int id)
        {
           return _aboutDal.GetById(id);
        }

        public void InsertBL(About entity)
        {
        _aboutDal.Insert(entity);
        }

        public List<About> List(Expression<Func<About, bool>> filter)
        {
            return _aboutDal.List(filter);
        }

        public void UpdateBL(About entity)
        {
        _aboutDal.Update(entity);
        }
    }
}
