using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos;

public sealed record NewsArticleDto(string NewsArticleId, string? NewsTitle, string Headline, DateTime? CreatedDate,
    string? NewsContent, string? NewsSource, short? CategoryId, bool? NewsStatus, short? CreatedById, short? UpdatedById,
    DateTime? ModifiedDate, IReadOnlyList<int> TagIds);

public sealed class NewsArticleRequest
{
    [Required, StringLength(20)] public string NewsArticleId { get; set; } = "";
    [StringLength(400)] public string? NewsTitle { get; set; }
    [Required, StringLength(150)] public string Headline { get; set; } = "";
    [Required, StringLength(4000)] public string NewsContent { get; set; } = "";
    [StringLength(400)] public string? NewsSource { get; set; }
    public short? CategoryId { get; set; }
    public bool NewsStatus { get; set; } = true;
    public short? CreatedById { get; set; }
    public short? UpdatedById { get; set; }
    public List<int> TagIds { get; set; } = [];
}
