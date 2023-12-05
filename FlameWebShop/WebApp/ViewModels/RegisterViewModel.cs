using System.ComponentModel.DataAnnotations;
using WebApp.Models.Dtos;

namespace WebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Förnamn")]
        [Required(ErrorMessage = "Du måste ange ett förnamn")]
        [RegularExpression(@"^[a-öA-Ö]+(?:['´-][a-öA-Ö]+)*$", ErrorMessage = "Du måste ange ett giltigt förnamn")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Efternamn")]
        [Required(ErrorMessage = "Du måste ange ett efternamn")]
        [RegularExpression(@"^[a-öA-Ö]+(?:['´-][a-öA-Ö]+)*$", ErrorMessage = "Du måste ange ett giltigt efternamn")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Email")]
        [Required(ErrorMessage = "You have to enter an email address")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Display(Name = "Password")]
        [Required(ErrorMessage = "You have to enter a password")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$", ErrorMessage = "Password is too weak. Include at least one number and one special sign")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Please confirm the password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password confirmation does not match given password.")]
        public string ConfirmPassword { get; set; } = null!;

        public static implicit operator RegisterModel(RegisterViewModel model)
        {
            return new RegisterModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
            };
        }
    }
}
