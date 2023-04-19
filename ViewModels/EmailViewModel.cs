using System.ComponentModel.DataAnnotations;

namespace ThomasGrant.ViewModels
{
    public class EmailViewModel
    {
        public string Title { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
    }
}
