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
    public class MessageManager : IMessageService
    {
        private readonly IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public void DeleteBL(Message2 entity)
        {
           _messageDal.Delete(entity);
        }

        public List<Message2> GetAllBL()
        {
            return _messageDal.GetAll();
        }

        public Message2 GetByIdBL(int id)
        {
          return  _messageDal.GetById(id);
        }

        public List<Message2> GetInboxListByWriterBL(int id)
        {
           return _messageDal.GetInboxListByWriter(id);
        }

        public void InsertBL(Message2 entity)
        {
            _messageDal.Insert(entity);
        }

        public List<Message2> List(Expression<Func<Message2, bool>> filter)
        {
            return _messageDal.List(filter);
        }

        public void UpdateBL(Message2 entity)
        {
            _messageDal.Update(entity);
        }
    }
}
