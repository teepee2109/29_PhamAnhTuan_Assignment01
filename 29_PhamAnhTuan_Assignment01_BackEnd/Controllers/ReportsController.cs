using BackEnd.Dtos;
using BackEnd.Services;
using Microsoft.AspNetCore.Mvc;
namespace BackEnd.Controllers;
[ApiController, Route("api/reports")]
public sealed class ReportsController(INewsArticleService service) : ControllerBase
{
    [HttpGet("news")]
    public async Task<ActionResult<IReadOnlyList<NewsArticleDto>>> News([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, CancellationToken ct)
        => startDate > endDate ? BadRequest("StartDate phải nhỏ hơn hoặc bằng EndDate.") : Ok(await service.ReportAsync(startDate, endDate, ct));
}
