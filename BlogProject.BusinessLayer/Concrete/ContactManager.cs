using BlogProject.BusinessLayer.Abstract;
using BlogProject.DataAccessLayer.Abstract;
using BlogProject.DataAccessLayer.EntityFramework;
using BlogProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.BusinessLayer.Concrete
{
    public class ContactManager : IContactService
    {
        private readonly IContactDal _contactDal;

        public ContactManager(IContactDal contactDal)
        {
            _contactDal = contactDal;
        }

        public void DeleteBL(Contact entity)
        {
           _contactDal.Delete(entity);
        }

        public List<Contact> GetAllBL()
        {
          return _contactDal.GetAll();
        }

        public Contact GetByIdBL(int id)
        {
            return _contactDal.GetById(id);
        }

        public void InsertBL(Contact entity)
        {
           _contactDal.Insert(entity);
        }

        public List<Contact> List(Expression<Func<Contact, bool>> filter)
        {
            return _contactDal.List(filter);
        }

        public void UpdateBL(Contact entity)
        {
            _contactDal.Update(entity);
        }
    }
}
