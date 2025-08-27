using BlogProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.BusinessLayer.Abstract
{
    public interface IMessageService:IGenericService<EntityLayer.Concrete.Message2>
    {
        List<Message2> GetInboxListByWriterBL(int id);
    }
}
