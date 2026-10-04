using BackEnd.BusinessObjects;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FUNewsManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<BackEnd.Repositories.IAccountRepository, BackEnd.Repositories.AccountRepository>();
builder.Services.AddScoped<BackEnd.Repositories.ICategoryRepository, BackEnd.Repositories.CategoryRepository>();
builder.Services.AddScoped<BackEnd.Repositories.INewsArticleRepository, BackEnd.Repositories.NewsArticleRepository>();
builder.Services.AddScoped<BackEnd.Repositories.ITagRepository, BackEnd.Repositories.TagRepository>();
builder.Services.AddScoped<BackEnd.Services.IAccountService, BackEnd.Services.AccountService>();
builder.Services.AddScoped<BackEnd.Services.ICategoryService, BackEnd.Services.CategoryService>();
builder.Services.AddScoped<BackEnd.Services.INewsArticleService, BackEnd.Services.NewsArticleService>();
builder.Services.AddScoped<BackEnd.Services.ITagService, BackEnd.Services.TagService>();
builder.Services.AddScoped<BackEnd.Services.IAuthService, BackEnd.Services.AuthService>();

builder.Services.AddControllers()
    .AddOData(options => options
        .Select()
        .Filter()
        .OrderBy()
        .Expand()
        .Count()
        .SetMaxTop(100));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
