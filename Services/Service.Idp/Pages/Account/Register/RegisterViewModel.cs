using System.ComponentModel.DataAnnotations;

namespace Service.Idp.Pages.Account.Register
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string? ReturnUrl { get; set; }
        public string RoleName { get; set; }
    }
}
