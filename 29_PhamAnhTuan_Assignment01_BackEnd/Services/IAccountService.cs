using BackEnd.Dtos;

namespace BackEnd.Services;

public interface IAccountService
{
    Task<IReadOnlyList<AccountDto>> SearchAsync(string? keyword, CancellationToken cancellationToken);
    Task<AccountDto?> GetAsync(short id, CancellationToken cancellationToken);
    Task<(bool Success, string? Error, AccountDto? Account)> CreateAsync(AccountRequest request, CancellationToken cancellationToken);
    Task<(bool Success, string? Error)> UpdateAsync(short id, AccountRequest request, CancellationToken cancellationToken);
    Task<(bool Success, string? Error)> DeleteAsync(short id, CancellationToken cancellationToken);
}
