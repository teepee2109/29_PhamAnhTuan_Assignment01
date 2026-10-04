using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos;

public sealed record AccountDto(short AccountId, string? AccountName, string? AccountEmail, int? AccountRole);

public sealed class AccountRequest
{
    [Required, StringLength(100)] public string AccountName { get; set; } = "";
    [Required, EmailAddress, StringLength(70)] public string AccountEmail { get; set; } = "";
    [Range(1, 2)] public int AccountRole { get; set; }
    [Required, StringLength(70, MinimumLength = 1)] public string AccountPassword { get; set; } = "";
}
