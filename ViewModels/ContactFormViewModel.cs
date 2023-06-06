using System.ComponentModel.DataAnnotations;

namespace ThomasGrant.ViewModels
{
    public class ContactFormViewModel
    {
        [Required(ErrorMessage = "I'd love to know who you are")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Let me know how to get in touch")]
        [EmailAddress]
        public string Email { get; set;}

        [Required(ErrorMessage = "Oops, looks like you forgot to fill in the message")]
        public string Message { get; set; }

        public bool? FileRequest { get; set; }

    }
}
