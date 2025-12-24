using System.ComponentModel.DataAnnotations;

namespace SecondHandProject.ViewModels.Profile
{
    public class ProfileEditVm
    {
        [Required]
        public string? FullName { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }
    }
}
