using BackEnd.Dtos;
using BackEnd.Services;
using Microsoft.AspNetCore.Mvc;
namespace BackEnd.Controllers;
[ApiController, Route("api/tags")]
public sealed class TagsController(ITagService service) : ControllerBase
{
    [HttpGet] public async Task<ActionResult<IReadOnlyList<TagDto>>> Get(string? search, CancellationToken ct) => Ok(await service.SearchAsync(search, ct));
    [HttpGet("{id:int}")] public async Task<ActionResult<TagDto>> Get(int id, CancellationToken ct) => await service.GetAsync(id, ct) is { } t ? Ok(t) : NotFound();
    [HttpPost] public async Task<ActionResult<TagDto>> Post(TagRequest request, CancellationToken ct) => Created("api/tags", await service.CreateAsync(request, ct));
    [HttpPut("{id:int}")] public async Task<IActionResult> Put(int id, TagRequest request, CancellationToken ct) => await service.UpdateAsync(id, request, ct) ? NoContent() : NotFound();
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id, CancellationToken ct) => await service.DeleteAsync(id, ct) ? NoContent() : Conflict("Tag không tồn tại hoặc đang được sử dụng.");
}
