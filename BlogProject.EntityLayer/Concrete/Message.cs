using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BlogProject.EntityLayer.Concrete
{
    
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }
}
