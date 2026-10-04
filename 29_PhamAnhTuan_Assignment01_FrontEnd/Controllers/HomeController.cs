using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _29_PhamAnhTuan_Assignment01_FrontEnd.Models;
using _29_PhamAnhTuan_Assignment01_FrontEnd.Services;

namespace _29_PhamAnhTuan_Assignment01_FrontEnd.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BackendApiClient _api;

    public HomeController(ILogger<HomeController> logger, BackendApiClient api)
    {
        _logger = logger;
        _api = api;
    }

    public async Task<IActionResult> Index(CancellationToken ct)
    {
        try { return View(await _api.GetPublicNewsAsync(ct)); }
        catch (HttpRequestException) { ViewBag.ApiUnavailable = true; return View(Array.Empty<NewsArticleViewModel>()); }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
