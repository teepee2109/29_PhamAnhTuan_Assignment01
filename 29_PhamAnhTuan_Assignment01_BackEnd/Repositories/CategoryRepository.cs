using BackEnd.BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories;

public sealed class CategoryRepository(FUNewsManagementContext context) : ICategoryRepository
{
    public async Task<IReadOnlyList<Category>> SearchAsync(string? keyword, CancellationToken cancellationToken)
    {
        IQueryable<Category> query = context.Categories.AsNoTracking().Include(c => c.ParentCategory);
        if (!string.IsNullOrWhiteSpace(keyword))
            query = query.Where(c => c.CategoryName.Contains(keyword) || c.CategoryDesciption.Contains(keyword));
        return await query.OrderBy(c => c.CategoryId).ToListAsync(cancellationToken);
    }

    public Task<Category?> GetByIdAsync(short id, CancellationToken cancellationToken) =>
        context.Categories.Include(c => c.ParentCategory).FirstOrDefaultAsync(c => c.CategoryId == id, cancellationToken);

    public async Task AddAsync(Category category, CancellationToken cancellationToken)
    {
        context.Categories.Add(category);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        context.Categories.Update(category);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(short id, CancellationToken cancellationToken)
    {
        var category = await context.Categories.Include(c => c.NewsArticles)
            .FirstOrDefaultAsync(c => c.CategoryId == id, cancellationToken);
        if (category is null || category.NewsArticles.Count != 0) return false;
        context.Categories.Remove(category);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
