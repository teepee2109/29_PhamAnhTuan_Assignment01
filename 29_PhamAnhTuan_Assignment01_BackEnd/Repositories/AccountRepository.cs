using BackEnd.BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories;

public sealed class AccountRepository(FUNewsManagementContext context) : IAccountRepository
{
    public async Task<IReadOnlyList<SystemAccount>> SearchAsync(string? keyword, CancellationToken cancellationToken)
    {
        var query = context.SystemAccounts.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(keyword))
            query = query.Where(a => a.AccountName!.Contains(keyword) || a.AccountEmail!.Contains(keyword));
        return await query.OrderBy(a => a.AccountId).ToListAsync(cancellationToken);
    }

    public Task<SystemAccount?> GetByIdAsync(short id, CancellationToken cancellationToken) =>
        context.SystemAccounts.FirstOrDefaultAsync(a => a.AccountId == id, cancellationToken);

    public Task<SystemAccount?> GetByEmailAsync(string email, CancellationToken cancellationToken) =>
        context.SystemAccounts.FirstOrDefaultAsync(a => a.AccountEmail == email, cancellationToken);

    public async Task AddAsync(SystemAccount account, CancellationToken cancellationToken)
    {
        context.SystemAccounts.Add(account);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SystemAccount account, CancellationToken cancellationToken)
    {
        context.SystemAccounts.Update(account);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(short id, CancellationToken cancellationToken)
    {
        var account = await context.SystemAccounts.Include(a => a.NewsArticles)
            .FirstOrDefaultAsync(a => a.AccountId == id, cancellationToken);
        if (account is null || account.NewsArticles.Count != 0) return false;
        context.SystemAccounts.Remove(account);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
