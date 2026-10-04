using BackEnd.BusinessObjects;
using BackEnd.Dtos;
using BackEnd.Repositories;
namespace BackEnd.Services;
public sealed class TagService(ITagRepository repository) : ITagService
{
    public async Task<IReadOnlyList<TagDto>> SearchAsync(string? search, CancellationToken ct) => (await repository.SearchAsync(search, ct)).Select(ToDto).ToList();
    public async Task<TagDto?> GetAsync(int id, CancellationToken ct) => (await repository.GetAsync(id, ct)) is { } t ? ToDto(t) : null;
    public async Task<TagDto> CreateAsync(TagRequest r, CancellationToken ct) { var t = new Tag { TagName = r.TagName, Note = r.Note }; await repository.AddAsync(t, ct); return ToDto(t); }
    public async Task<bool> UpdateAsync(int id, TagRequest r, CancellationToken ct) { var t = await repository.GetAsync(id, ct); if (t is null) return false; t.TagName = r.TagName; t.Note = r.Note; await repository.UpdateAsync(t, ct); return true; }
    public Task<bool> DeleteAsync(int id, CancellationToken ct) => repository.DeleteAsync(id, ct);
    private static TagDto ToDto(Tag t) => new(t.TagId, t.TagName, t.Note);
}
