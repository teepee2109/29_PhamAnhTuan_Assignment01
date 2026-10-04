using BackEnd.BusinessObjects;
using BackEnd.Dtos;
using BackEnd.Repositories;

namespace BackEnd.Services;

public sealed class AccountService(IAccountRepository repository) : IAccountService
{
    public async Task<IReadOnlyList<AccountDto>> SearchAsync(string? keyword, CancellationToken ct) =>
        (await repository.SearchAsync(keyword, ct)).Select(ToDto).ToList();

    public async Task<AccountDto?> GetAsync(short id, CancellationToken ct) =>
        (await repository.GetByIdAsync(id, ct)) is { } a ? ToDto(a) : null;

    public async Task<(bool Success, string? Error, AccountDto? Account)> CreateAsync(AccountRequest request, CancellationToken ct)
    {
        if (await repository.GetByEmailAsync(request.AccountEmail, ct) is not null)
            return (false, "Email đã tồn tại.", null);
        var account = new SystemAccount { AccountName = request.AccountName, AccountEmail = request.AccountEmail, AccountRole = request.AccountRole, AccountPassword = request.AccountPassword };
        await repository.AddAsync(account, ct);
        return (true, null, ToDto(account));
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(short id, AccountRequest request, CancellationToken ct)
    {
        var account = await repository.GetByIdAsync(id, ct);
        if (account is null) return (false, "Không tìm thấy account.");
        var duplicate = await repository.GetByEmailAsync(request.AccountEmail, ct);
        if (duplicate is not null && duplicate.AccountId != id) return (false, "Email đã tồn tại.");
        account.AccountName = request.AccountName; account.AccountEmail = request.AccountEmail; account.AccountRole = request.AccountRole; account.AccountPassword = request.AccountPassword;
        await repository.UpdateAsync(account, ct);
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> DeleteAsync(short id, CancellationToken ct) =>
        await repository.DeleteAsync(id, ct) ? (true, null) : (false, "Account không tồn tại hoặc đã tạo news article nên không thể xóa.");

    private static AccountDto ToDto(SystemAccount a) => new(a.AccountId, a.AccountName, a.AccountEmail, a.AccountRole);
}
