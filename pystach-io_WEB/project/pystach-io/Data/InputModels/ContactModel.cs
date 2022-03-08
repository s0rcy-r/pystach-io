using System.ComponentModel.DataAnnotations;

namespace pystach_io.Data.InputModels
{
    public class ContactModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [DataType(DataType.Text)]
        public string content { get; set; }
    }
}
