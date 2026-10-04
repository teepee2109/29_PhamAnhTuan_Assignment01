using System.ComponentModel.DataAnnotations;
namespace BackEnd.Dtos;
public sealed record TagDto(int TagId, string? TagName, string? Note);
public sealed class TagRequest { [Required, StringLength(50)] public string TagName { get; set; } = ""; [StringLength(400)] public string? Note { get; set; } }
