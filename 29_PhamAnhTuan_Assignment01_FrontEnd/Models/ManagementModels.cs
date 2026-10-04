namespace _29_PhamAnhTuan_Assignment01_FrontEnd.Models;
public sealed record CategoryViewModel(short CategoryId, string CategoryName, string CategoryDesciption, short? ParentCategoryId, bool? IsActive);
public sealed record TagViewModel(int TagId, string? TagName, string? Note);
public sealed class TagFormModel { public string TagName { get; set; } = ""; public string? Note { get; set; } }
public sealed class CategoryFormModel { public string CategoryName { get; set; } = ""; public string CategoryDesciption { get; set; } = ""; public bool IsActive { get; set; } = true; }
public sealed class NewsFormModel { public string NewsArticleId { get; set; } = ""; public string? NewsTitle { get; set; } public string Headline { get; set; } = ""; public string NewsContent { get; set; } = ""; public string? NewsSource { get; set; } public short? CategoryId { get; set; } public bool NewsStatus { get; set; } = true; public short? CreatedById { get; set; } public List<int> TagIds { get; set; } = []; }
