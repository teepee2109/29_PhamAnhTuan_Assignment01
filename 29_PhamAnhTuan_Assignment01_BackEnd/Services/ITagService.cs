using BackEnd.Dtos;
namespace BackEnd.Services;
public interface ITagService { Task<IReadOnlyList<TagDto>> SearchAsync(string? search, CancellationToken ct); Task<TagDto?> GetAsync(int id, CancellationToken ct); Task<TagDto> CreateAsync(TagRequest request, CancellationToken ct); Task<bool> UpdateAsync(int id, TagRequest request, CancellationToken ct); Task<bool> DeleteAsync(int id, CancellationToken ct); }
