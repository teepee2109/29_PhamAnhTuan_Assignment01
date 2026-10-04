using BackEnd.Dtos;
using BackEnd.Repositories;
using Microsoft.Extensions.Configuration;

namespace BackEnd.Services;

public sealed class AuthService(IAccountRepository accounts, IConfiguration configuration) : IAuthService
{
    public async Task<LoginResponse?> LoginAsync(LoginRequest request, CancellationToken ct)
    {
        var adminEmail = configuration["AdminAccount:Email"];
        var adminPassword = configuration["AdminAccount:Password"];
        if (request.Email.Equals(adminEmail, StringComparison.OrdinalIgnoreCase) && request.Password == adminPassword)
            return new LoginResponse(null, request.Email, "Admin", true);

        var account = await accounts.GetByEmailAsync(request.Email, ct);
        if (account is null || account.AccountPassword != request.Password) return null;
        var role = account.AccountRole == 1 ? "Staff" : "Lecturer";
        return new LoginResponse(account.AccountId, account.AccountEmail!, role, false);
    }
}
