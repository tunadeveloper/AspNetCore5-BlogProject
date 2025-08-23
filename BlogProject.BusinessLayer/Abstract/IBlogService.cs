using BlogProject.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.BusinessLayer.Abstract
{
    public interface IBlogService:IGenericService<Blog>
    {
        List<Blog> GetListWithCategoryBL();
        Blog GetByIdWithCategoryBL(int id);
        List<Blog> GetBlogListByWriterBL(int id);
        List<Blog> GetLastBlogBL();
    }
}
