using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.EntityLayer.Concrete
{
    public class Newsletter
    {
        [Key]
        public int NewsletterId { get; set; }
        public string Email { get; set; }
        public bool EmailStaus { get; set; }
    }

}
