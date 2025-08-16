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
    public class CommentManager : ICommentService
    {
        private readonly ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public void DeleteBL(Comment entity)
        {
            _commentDal.Delete(entity);
        }

        public List<Comment> GetAllBL()
        {
            return _commentDal.GetAll();
        }

        public Comment GetByIdBL(int id)
        {
           return _commentDal.GetById(id);
        }

        public void InsertBL(Comment entity)
        {
           _commentDal.Insert(entity);
        }

        public List<Comment> List(Expression<Func<Comment, bool>> filter)
        {
            return _commentDal.List(filter);
        }

        public void UpdateBL(Comment entity)
        {
           _commentDal.Update(entity);
        }
    }
}
