var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("Backend", client => client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7051/"))
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        // The Backend uses the local ASP.NET Core development certificate.
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    });
builder.Services.AddSingleton<_29_PhamAnhTuan_Assignment01_FrontEnd.Services.BackendApiClient>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => { options.Cookie.HttpOnly = true; options.Cookie.IsEssential = true; });
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
