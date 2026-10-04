using BackEnd.BusinessObjects;

namespace BackEnd.Repositories;

public interface INewsArticleRepository
{
    Task<IReadOnlyList<NewsArticle>> SearchAsync(string? keyword, bool activeOnly, CancellationToken cancellationToken);
    Task<NewsArticle?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task AddAsync(NewsArticle article, IReadOnlyCollection<int> tagIds, CancellationToken cancellationToken);
    Task UpdateAsync(NewsArticle article, IReadOnlyCollection<int> tagIds, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(string id, CancellationToken cancellationToken);
    Task<IReadOnlyList<NewsArticle>> ReportAsync(DateTime start, DateTime end, CancellationToken cancellationToken);
}
