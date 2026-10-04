using BackEnd.BusinessObjects;
using BackEnd.Dtos;
using BackEnd.Repositories;

namespace BackEnd.Services;

public sealed class NewsArticleService(INewsArticleRepository repository) : INewsArticleService
{
    public async Task<IReadOnlyList<NewsArticleDto>> SearchAsync(string? keyword, bool activeOnly, CancellationToken ct) => (await repository.SearchAsync(keyword, activeOnly, ct)).Select(ToDto).ToList();
    public async Task<NewsArticleDto?> GetAsync(string id, CancellationToken ct) => (await repository.GetByIdAsync(id, ct)) is { } n ? ToDto(n) : null;
    public async Task<(bool Success, string? Error)> CreateAsync(NewsArticleRequest r, CancellationToken ct)
    {
        if (await repository.GetByIdAsync(r.NewsArticleId, ct) is not null) return (false, "News article ID đã tồn tại.");
        await repository.AddAsync(ToEntity(r), r.TagIds, ct); return (true, null);
    }
    public async Task<(bool Success, string? Error)> UpdateAsync(string id, NewsArticleRequest r, CancellationToken ct)
    {
        var current = await repository.GetByIdAsync(id, ct); if (current is null) return (false, "Không tìm thấy news article.");
        var entity = ToEntity(r); entity.NewsArticleId = id; entity.CreatedDate = current.CreatedDate; await repository.UpdateAsync(entity, r.TagIds, ct); return (true, null);
    }
    public async Task<(bool Success, string? Error)> DeleteAsync(string id, CancellationToken ct) => await repository.DeleteAsync(id, ct) ? (true, null) : (false, "Không tìm thấy news article.");
    public async Task<IReadOnlyList<NewsArticleDto>> ReportAsync(DateTime start, DateTime end, CancellationToken ct) => (await repository.ReportAsync(start, end, ct)).Select(ToDto).ToList();
    private static NewsArticle ToEntity(NewsArticleRequest r) => new() { NewsArticleId = r.NewsArticleId, NewsTitle = r.NewsTitle, Headline = r.Headline, CreatedDate = DateTime.UtcNow, NewsContent = r.NewsContent, NewsSource = r.NewsSource, CategoryId = r.CategoryId, NewsStatus = r.NewsStatus, CreatedById = r.CreatedById, UpdatedById = r.UpdatedById, ModifiedDate = DateTime.UtcNow };
    private static NewsArticleDto ToDto(NewsArticle n) => new(n.NewsArticleId, n.NewsTitle, n.Headline, n.CreatedDate, n.NewsContent, n.NewsSource, n.CategoryId, n.NewsStatus, n.CreatedById, n.UpdatedById, n.ModifiedDate, n.Tags.Select(t => t.TagId).ToList());
}
