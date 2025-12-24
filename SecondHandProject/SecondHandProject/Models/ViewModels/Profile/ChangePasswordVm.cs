using System.ComponentModel.DataAnnotations;

namespace SecondHandProject.ViewModels.Profile
{
    public class ChangePasswordVm
    {
        [Required, DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = "";

        [Required, MinLength(6), DataType(DataType.Password)]
        public string NewPassword { get; set; } = "";

        [Required, Compare(nameof(NewPassword)), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = "";
    }
}
