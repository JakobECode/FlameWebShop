using System.ComponentModel.DataAnnotations;
using WebApp.Models.Dtos;

namespace WebApp.ViewModels
{
    public class ContactFormViewModel
    {
        [Display(Name = "Your Name")]
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; } = null!;

        [Display(Name = "Your Email")]
        [Required(ErrorMessage = "Please enter your email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Display(Name = "Comments")]
        [Required(ErrorMessage = "Please enter a comment")]
        public string CommentsText { get; set; } = null!;


        public static implicit operator Comment(ContactFormViewModel model)
        {
            return new Comment()
            {
                Name = model.Name,
                Email = model.Email,
                CommentsText = model.CommentsText
            };
        }
    }
}
