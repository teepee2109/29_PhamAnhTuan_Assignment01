using System.ComponentModel.DataAnnotations;

namespace BackEnd.Dtos;

public sealed record CategoryDto(short CategoryId, string CategoryName, string CategoryDesciption, short? ParentCategoryId, bool? IsActive);

public sealed class CategoryRequest
{
    [Required, StringLength(100)] public string CategoryName { get; set; } = "";
    [Required, StringLength(250)] public string CategoryDesciption { get; set; } = "";
    public short? ParentCategoryId { get; set; }
    public bool IsActive { get; set; } = true;
}
