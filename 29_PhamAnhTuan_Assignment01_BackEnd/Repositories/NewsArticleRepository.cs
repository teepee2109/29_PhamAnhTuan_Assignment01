using BackEnd.BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories;

public sealed class NewsArticleRepository(FUNewsManagementContext context) : INewsArticleRepository
{
    public async Task<IReadOnlyList<NewsArticle>> SearchAsync(string? keyword, bool activeOnly, CancellationToken cancellationToken)
    {
        var query = context.NewsArticles.AsNoTracking()
            .Include(n => n.Category)
            .Include(n => n.CreatedBy)
            .Include(n => n.Tags)
            .AsQueryable();
        if (activeOnly) query = query.Where(n => n.NewsStatus == true);
        if (!string.IsNullOrWhiteSpace(keyword))
            query = query.Where(n => n.NewsTitle!.Contains(keyword) || n.Headline.Contains(keyword) || n.NewsContent!.Contains(keyword));
        return await query.OrderByDescending(n => n.CreatedDate).ToListAsync(cancellationToken);
    }

    public Task<NewsArticle?> GetByIdAsync(string id, CancellationToken cancellationToken) =>
        context.NewsArticles.Include(n => n.Category).Include(n => n.CreatedBy).Include(n => n.Tags)
            .FirstOrDefaultAsync(n => n.NewsArticleId == id, cancellationToken);

    public async Task AddAsync(NewsArticle article, IReadOnlyCollection<int> tagIds, CancellationToken cancellationToken)
    {
        article.Tags = await context.Tags.Where(t => tagIds.Contains(t.TagId)).ToListAsync(cancellationToken);
        context.NewsArticles.Add(article);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(NewsArticle article, IReadOnlyCollection<int> tagIds, CancellationToken cancellationToken)
    {
        article.Tags = await context.Tags.Where(t => tagIds.Contains(t.TagId)).ToListAsync(cancellationToken);
        context.NewsArticles.Update(article);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var article = await context.NewsArticles.FirstOrDefaultAsync(n => n.NewsArticleId == id, cancellationToken);
        if (article is null) return false;
        context.NewsArticles.Remove(article);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<IReadOnlyList<NewsArticle>> ReportAsync(DateTime start, DateTime end, CancellationToken cancellationToken) =>
        await context.NewsArticles.AsNoTracking().Include(n => n.CreatedBy).Where(n => n.CreatedDate >= start && n.CreatedDate <= end).OrderByDescending(n => n.CreatedDate).ToListAsync(cancellationToken);
}
