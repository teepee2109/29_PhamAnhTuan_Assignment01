using BackEnd.Dtos;
using BackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers;

[ApiController, Route("api/categories")]
public sealed class CategoriesController(ICategoryService service) : ControllerBase
{
    [HttpGet] public async Task<ActionResult<IReadOnlyList<CategoryDto>>> Get([FromQuery] string? search, CancellationToken ct) => Ok(await service.SearchAsync(search, ct));
    [HttpGet("{id:int}")] public async Task<ActionResult<CategoryDto>> Get(short id, CancellationToken ct) => await service.GetAsync(id, ct) is { } c ? Ok(c) : NotFound();
    [HttpPost] public async Task<ActionResult<CategoryDto>> Post(CategoryRequest request, CancellationToken ct) { var r = await service.CreateAsync(request, ct); return Created("api/categories", r.Category); }
    [HttpPut("{id:int}")] public async Task<IActionResult> Put(short id, CategoryRequest request, CancellationToken ct) { var r = await service.UpdateAsync(id, request, ct); return r.Success ? NoContent() : BadRequest(r.Error); }
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(short id, CancellationToken ct) { var r = await service.DeleteAsync(id, ct); return r.Success ? NoContent() : Conflict(r.Error); }
}
