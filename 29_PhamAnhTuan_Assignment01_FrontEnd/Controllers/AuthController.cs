using _29_PhamAnhTuan_Assignment01_FrontEnd.Models;
using _29_PhamAnhTuan_Assignment01_FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace _29_PhamAnhTuan_Assignment01_FrontEnd.Controllers;

public sealed class AuthController(BackendApiClient api) : Controller
{
    [HttpGet] public IActionResult Login(string? returnUrl = null) => View(new LoginViewModel());
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(model);
        var account = await api.LoginAsync(model, ct);
        if (account is null) { ModelState.AddModelError("", "Email hoặc password không đúng."); return View(model); }
        HttpContext.Session.SetString("UserEmail", account.Email); HttpContext.Session.SetString("UserRole", account.Role);
        return Redirect(returnUrl is { Length: > 0 } && Url.IsLocalUrl(returnUrl) ? returnUrl : Url.Action("Index", "Home")!);
    }
    [HttpPost, ValidateAntiForgeryToken] public IActionResult Logout() { HttpContext.Session.Clear(); return RedirectToAction("Index", "Home"); }
}
