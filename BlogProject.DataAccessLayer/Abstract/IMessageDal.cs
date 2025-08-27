using BlogProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DataAccessLayer.Abstract
{
    public interface IMessageDal:IGenericDal<EntityLayer.Concrete.Message2>
    {
       List<Message2> GetInboxListByWriter(int id);
    }
}
