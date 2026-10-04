using BackEnd.Dtos;

namespace BackEnd.Services;

public interface ICategoryService
{
    Task<IReadOnlyList<CategoryDto>> SearchAsync(string? keyword, CancellationToken ct);
    Task<CategoryDto?> GetAsync(short id, CancellationToken ct);
    Task<(bool Success, string? Error, CategoryDto? Category)> CreateAsync(CategoryRequest request, CancellationToken ct);
    Task<(bool Success, string? Error)> UpdateAsync(short id, CategoryRequest request, CancellationToken ct);
    Task<(bool Success, string? Error)> DeleteAsync(short id, CancellationToken ct);
}
