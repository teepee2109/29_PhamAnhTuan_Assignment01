using System.ComponentModel.DataAnnotations;

namespace _29_PhamAnhTuan_Assignment01_FrontEnd.Models;

public sealed class LoginViewModel
{
    [Required, EmailAddress] public string Email { get; set; } = "";
    [Required, DataType(DataType.Password)] public string Password { get; set; } = "";
}

public sealed record LoginResponse(short? AccountId, string Email, string Role, bool IsAdmin);
public sealed record AccountViewModel(short AccountId, string? AccountName, string? AccountEmail, int? AccountRole);
public sealed class AccountFormModel { public string AccountName { get; set; } = ""; public string AccountEmail { get; set; } = ""; public int AccountRole { get; set; } = 1; public string AccountPassword { get; set; } = ""; }
