using BackEnd.BusinessObjects;

namespace BackEnd.Repositories;

public interface IAccountRepository
{
    Task<IReadOnlyList<SystemAccount>> SearchAsync(string? keyword, CancellationToken cancellationToken);
    Task<SystemAccount?> GetByIdAsync(short id, CancellationToken cancellationToken);
    Task<SystemAccount?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task AddAsync(SystemAccount account, CancellationToken cancellationToken);
    Task UpdateAsync(SystemAccount account, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(short id, CancellationToken cancellationToken);
}
