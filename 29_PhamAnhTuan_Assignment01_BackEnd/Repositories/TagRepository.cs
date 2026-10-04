using BackEnd.BusinessObjects;
using Microsoft.EntityFrameworkCore;
namespace BackEnd.Repositories;
public sealed class TagRepository(FUNewsManagementContext context) : ITagRepository
{
    public async Task<IReadOnlyList<Tag>> SearchAsync(string? search, CancellationToken ct) { var q = context.Tags.AsNoTracking(); if (!string.IsNullOrWhiteSpace(search)) q = q.Where(t => t.TagName!.Contains(search)); return await q.OrderBy(t => t.TagId).ToListAsync(ct); }
    public Task<Tag?> GetAsync(int id, CancellationToken ct) => context.Tags.Include(t => t.NewsArticles).FirstOrDefaultAsync(t => t.TagId == id, ct);
    public async Task AddAsync(Tag tag, CancellationToken ct) { context.Tags.Add(tag); await context.SaveChangesAsync(ct); }
    public async Task UpdateAsync(Tag tag, CancellationToken ct) { context.Tags.Update(tag); await context.SaveChangesAsync(ct); }
    public async Task<bool> DeleteAsync(int id, CancellationToken ct) { var t = await GetAsync(id, ct); if (t is null || t.NewsArticles.Count > 0) return false; context.Tags.Remove(t); await context.SaveChangesAsync(ct); return true; }
}
