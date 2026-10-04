using BackEnd.BusinessObjects;
using BackEnd.Dtos;
using BackEnd.Repositories;

namespace BackEnd.Services;

public sealed class CategoryService(ICategoryRepository repository) : ICategoryService
{
    public async Task<IReadOnlyList<CategoryDto>> SearchAsync(string? keyword, CancellationToken ct) => (await repository.SearchAsync(keyword, ct)).Select(ToDto).ToList();
    public async Task<CategoryDto?> GetAsync(short id, CancellationToken ct) => (await repository.GetByIdAsync(id, ct)) is { } c ? ToDto(c) : null;
    public async Task<(bool Success, string? Error, CategoryDto? Category)> CreateAsync(CategoryRequest r, CancellationToken ct)
    {
        var c = new Category { CategoryName = r.CategoryName, CategoryDesciption = r.CategoryDesciption, ParentCategoryId = r.ParentCategoryId, IsActive = r.IsActive };
        await repository.AddAsync(c, ct); return (true, null, ToDto(c));
    }
    public async Task<(bool Success, string? Error)> UpdateAsync(short id, CategoryRequest r, CancellationToken ct)
    {
        var c = await repository.GetByIdAsync(id, ct); if (c is null) return (false, "Không tìm thấy category.");
        c.CategoryName = r.CategoryName; c.CategoryDesciption = r.CategoryDesciption; c.ParentCategoryId = r.ParentCategoryId; c.IsActive = r.IsActive;
        await repository.UpdateAsync(c, ct); return (true, null);
    }
    public async Task<(bool Success, string? Error)> DeleteAsync(short id, CancellationToken ct) => await repository.DeleteAsync(id, ct) ? (true, null) : (false, "Category không tồn tại hoặc đã được sử dụng trong news article.");
    private static CategoryDto ToDto(Category c) => new(c.CategoryId, c.CategoryName, c.CategoryDesciption, c.ParentCategoryId, c.IsActive);
}
