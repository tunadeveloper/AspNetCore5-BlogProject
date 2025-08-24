using System.ComponentModel.DataAnnotations;

namespace BlogProject.PresentationLayer.Models
{
    public class WriterUpdateViewModel
    {
        public int WriterId { get; set; }
        public string WriterName { get; set; }
        public string WriterEmail { get; set; }
        public string WriterImage { get; set; }

        [DataType(DataType.Password)]
        public string WriterPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("WriterPassword", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        public string WriterAbout { get; set; }
    }
}
