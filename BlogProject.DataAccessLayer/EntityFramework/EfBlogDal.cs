using BlogProject.DataAccessLayer.Abstract;
using BlogProject.DataAccessLayer.Concrete;
using BlogProject.DataAccessLayer.Repositories;
using BlogProject.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DataAccessLayer.EntityFramework
{
    public class EfBlogDal : GenericRepository<Blog>, IBlogDal
    {
        private readonly Context _context;
        public EfBlogDal(Context context) : base(context)
        {
            _context = context;
        }

        public List<Blog> GetListWithCategory()
        {
            return _context.Blogs
                .Include(b => b.Category)
                .ToList();
        }
    }
}
