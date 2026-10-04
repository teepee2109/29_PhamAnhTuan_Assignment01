using System.Net.Http.Json;
using _29_PhamAnhTuan_Assignment01_FrontEnd.Models;

namespace _29_PhamAnhTuan_Assignment01_FrontEnd.Services;

public sealed class BackendApiClient(IHttpClientFactory factory)
{
    private HttpClient Client => factory.CreateClient("Backend");
    public async Task<LoginResponse?> LoginAsync(LoginViewModel model, CancellationToken ct)
    {
        using var response = await Client.PostAsJsonAsync("api/auth/login", model, ct);
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken: ct) : null;
    }
    public async Task<IReadOnlyList<NewsArticleViewModel>> GetPublicNewsAsync(CancellationToken ct) =>
        await Client.GetFromJsonAsync<List<NewsArticleViewModel>>("api/news-articles/public", ct) ?? [];
    public async Task<List<AccountViewModel>> AccountsAsync(string? search, CancellationToken ct) => await Client.GetFromJsonAsync<List<AccountViewModel>>($"api/accounts?search={Uri.EscapeDataString(search ?? "")}", ct) ?? [];
    public async Task<List<CategoryViewModel>> CategoriesAsync(string? search, CancellationToken ct) => await Client.GetFromJsonAsync<List<CategoryViewModel>>($"api/categories?search={Uri.EscapeDataString(search ?? "")}", ct) ?? [];
    public async Task<List<NewsArticleViewModel>> NewsAsync(string? search, CancellationToken ct) => await Client.GetFromJsonAsync<List<NewsArticleViewModel>>($"api/news-articles?search={Uri.EscapeDataString(search ?? "")}", ct) ?? [];
    public async Task<List<TagViewModel>> TagsAsync(string? search, CancellationToken ct) => await Client.GetFromJsonAsync<List<TagViewModel>>($"api/tags?search={Uri.EscapeDataString(search ?? "")}", ct) ?? [];
    public async Task<bool> PostAsync(string url, object body, CancellationToken ct) => (await Client.PostAsJsonAsync(url, body, ct)).IsSuccessStatusCode;
    public async Task<bool> DeleteAsync(string url, CancellationToken ct) => (await Client.DeleteAsync(url, ct)).IsSuccessStatusCode;
    public async Task<bool> PutAsync(string url, object body, CancellationToken ct) => (await Client.PutAsJsonAsync(url, body, ct)).IsSuccessStatusCode;
    public async Task<List<NewsArticleViewModel>> ReportAsync(DateTime start, DateTime end, CancellationToken ct) => await Client.GetFromJsonAsync<List<NewsArticleViewModel>>($"api/reports/news?startDate={start:O}&endDate={end:O}", ct) ?? [];
}
