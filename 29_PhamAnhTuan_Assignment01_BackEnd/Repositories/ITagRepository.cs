using BackEnd.BusinessObjects;
namespace BackEnd.Repositories;
public interface ITagRepository { Task<IReadOnlyList<Tag>> SearchAsync(string? search, CancellationToken ct); Task<Tag?> GetAsync(int id, CancellationToken ct); Task AddAsync(Tag tag, CancellationToken ct); Task UpdateAsync(Tag tag, CancellationToken ct); Task<bool> DeleteAsync(int id, CancellationToken ct); }
