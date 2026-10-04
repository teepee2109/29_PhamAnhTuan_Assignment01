namespace _29_PhamAnhTuan_Assignment01_FrontEnd.Models;

public sealed record NewsArticleViewModel(string NewsArticleId, string? NewsTitle, string Headline, DateTime? CreatedDate,
    string? NewsContent, string? NewsSource, short? CategoryId, bool? NewsStatus, short? CreatedById, short? UpdatedById,
    DateTime? ModifiedDate, IReadOnlyList<int> TagIds);
