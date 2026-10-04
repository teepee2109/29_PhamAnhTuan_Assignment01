using BackEnd.BusinessObjects;

namespace BackEnd.Repositories;

public interface ICategoryRepository
{
    Task<IReadOnlyList<Category>> SearchAsync(string? keyword, CancellationToken cancellationToken);
    Task<Category?> GetByIdAsync(short id, CancellationToken cancellationToken);
    Task AddAsync(Category category, CancellationToken cancellationToken);
    Task UpdateAsync(Category category, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(short id, CancellationToken cancellationToken);
}
