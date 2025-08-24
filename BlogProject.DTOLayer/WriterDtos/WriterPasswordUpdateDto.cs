using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.DTOLayer.WriterDtos
{
    public class WriterPasswordUpdateDto
    {
        public int WriterId { get; set; }

        public string WriterName { get; set; }
        public string WriterEmail { get; set; }
        public string WriterAbout { get; set; }
        public string WriterImage { get; set; }

        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
