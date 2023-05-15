using System.ComponentModel.DataAnnotations;

namespace Pustok_Web_App.ViewModel.Account
{
    public class RegistrVM
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string UserName { get; set; } = null!;
        [MaxLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [MaxLength(20), DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [MaxLength(20), DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;
    }
}
