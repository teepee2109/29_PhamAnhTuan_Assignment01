using BackEnd.Dtos;
using BackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers;

[ApiController, Route("api/accounts")]
public sealed class AccountsController(IAccountService service) : ControllerBase
{
    [HttpGet] public async Task<ActionResult<IReadOnlyList<AccountDto>>> Get([FromQuery] string? search, CancellationToken ct) => Ok(await service.SearchAsync(search, ct));
    [HttpGet("{id:int}")] public async Task<ActionResult<AccountDto>> Get(short id, CancellationToken ct) => await service.GetAsync(id, ct) is { } a ? Ok(a) : NotFound();
    [HttpPost] public async Task<ActionResult<AccountDto>> Post(AccountRequest request, CancellationToken ct)
    {
        var result = await service.CreateAsync(request, ct); if (!result.Success) return Conflict(result.Error); return CreatedAtAction(nameof(Get), new { id = result.Account!.AccountId }, result.Account);
    }
    [HttpPut("{id:int}")] public async Task<IActionResult> Put(short id, AccountRequest request, CancellationToken ct) { var result = await service.UpdateAsync(id, request, ct); return result.Success ? NoContent() : BadRequest(result.Error); }
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(short id, CancellationToken ct) { var result = await service.DeleteAsync(id, ct); return result.Success ? NoContent() : Conflict(result.Error); }
}
