using System.ComponentModel.DataAnnotations;

namespace Pustok_Web_App.ViewModel.Account;

public class LoginVM
{
    public string EmailOrUsername { get; set; } = null!;
    [MaxLength(255), DataType(DataType.Password)]
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; }
}
