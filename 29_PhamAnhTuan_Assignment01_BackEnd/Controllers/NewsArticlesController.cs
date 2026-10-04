using BackEnd.Dtos;
using BackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers;

[ApiController, Route("api/news-articles")]
public sealed class NewsArticlesController(INewsArticleService service) : ControllerBase
{
    [HttpGet] public async Task<ActionResult<IReadOnlyList<NewsArticleDto>>> Get([FromQuery] string? search, [FromQuery] bool activeOnly = false, CancellationToken ct = default) => Ok(await service.SearchAsync(search, activeOnly, ct));
    [HttpGet("public")] public async Task<ActionResult<IReadOnlyList<NewsArticleDto>>> Public(CancellationToken ct) => Ok(await service.SearchAsync(null, true, ct));
    [HttpGet("{id}")] public async Task<ActionResult<NewsArticleDto>> Get(string id, CancellationToken ct) => await service.GetAsync(id, ct) is { } n ? Ok(n) : NotFound();
    [HttpPost] public async Task<IActionResult> Post(NewsArticleRequest request, CancellationToken ct) { var r = await service.CreateAsync(request, ct); return r.Success ? CreatedAtAction(nameof(Get), new { id = request.NewsArticleId }, request) : Conflict(r.Error); }
    [HttpPut("{id}")] public async Task<IActionResult> Put(string id, NewsArticleRequest request, CancellationToken ct) { var r = await service.UpdateAsync(id, request, ct); return r.Success ? NoContent() : BadRequest(r.Error); }
    [HttpDelete("{id}")] public async Task<IActionResult> Delete(string id, CancellationToken ct) { var r = await service.DeleteAsync(id, ct); return r.Success ? NoContent() : NotFound(r.Error); }
}
