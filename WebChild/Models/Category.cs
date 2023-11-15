using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebChild.Data;

public class Category
{
    [Key]
    public int id { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string? CategoryName { get; set; }

    [Column(TypeName = "nvarchar(500)")]
    public string? CategoryDescription { get; set; }
    
    public ICollection<Product>? Products { get; set; }
}