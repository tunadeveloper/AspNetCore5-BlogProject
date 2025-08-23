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
    public class NewsletterManager : INewsletterService
    {
        private readonly INewsletterDal _newsletterDal;

        public NewsletterManager(INewsletterDal newsletterDal)
        {
            _newsletterDal = newsletterDal;
        }

        public void DeleteBL(Newsletter entity)
        {
           _newsletterDal.Delete(entity);
        }

        public List<Newsletter> GetAllBL()
        {
          return _newsletterDal.GetAll();
        }

        public Newsletter GetByIdBL(int id)
        {
            return _newsletterDal.GetById(id);
        }

        public void InsertBL(Newsletter entity)
        {
           _newsletterDal.Insert(entity);
        }

        public List<Newsletter> List(Expression<Func<Newsletter, bool>> filter)
        {
            return _newsletterDal.List(filter);
        }

        public void UpdateBL(Newsletter entity)
        {
          _newsletterDal.Update(entity);
        }
    }
}
