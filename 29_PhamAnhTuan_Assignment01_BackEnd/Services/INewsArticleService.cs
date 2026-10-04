using BackEnd.Dtos;

namespace BackEnd.Services;

public interface INewsArticleService
{
    Task<IReadOnlyList<NewsArticleDto>> SearchAsync(string? keyword, bool activeOnly, CancellationToken ct);
    Task<NewsArticleDto?> GetAsync(string id, CancellationToken ct);
    Task<(bool Success, string? Error)> CreateAsync(NewsArticleRequest request, CancellationToken ct);
    Task<(bool Success, string? Error)> UpdateAsync(string id, NewsArticleRequest request, CancellationToken ct);
    Task<(bool Success, string? Error)> DeleteAsync(string id, CancellationToken ct);
    Task<IReadOnlyList<NewsArticleDto>> ReportAsync(DateTime start, DateTime end, CancellationToken ct);
}
