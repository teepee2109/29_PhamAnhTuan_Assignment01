using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos;

public sealed class LoginRequest
{
    [Required, EmailAddress] public string Email { get; set; } = "";
    [Required] public string Password { get; set; } = "";
}

public sealed record LoginResponse(short? AccountId, string Email, string Role, bool IsAdmin);
