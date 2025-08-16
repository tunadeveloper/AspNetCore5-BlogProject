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
    public class WriterManager : IWriterService
    {
        private readonly IWriterDal _writerDal;

        public WriterManager(IWriterDal writerDal)
        {
            _writerDal = writerDal;
        }

        public void DeleteBL(Writer entity)
        {
           _writerDal.Delete(entity);
        }

        public List<Writer> GetAllBL()
        {
            return _writerDal.GetAll();
        }

        public Writer GetByIdBL(int id)
        {
          return _writerDal.GetById(id);
        }

        public void InsertBL(Writer entity)
        {
           _writerDal.Insert(entity);
        }

        public List<Writer> List(Expression<Func<Writer, bool>> filter)
        {
            return _writerDal.List(filter);
        }

        public void UpdateBL(Writer entity)
        {
           _writerDal.Update(entity);
        }
    }
}
