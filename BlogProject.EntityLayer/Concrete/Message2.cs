using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.EntityLayer.Concrete
{
    public class Message2
    {
        [Key]
        public int MessageId { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }

        public Writer SenderUser { get; set; }
        public Writer ReceiverUser { get; set; }
    }
}
