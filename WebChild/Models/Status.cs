using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebChild.Data;

public class Status
{
    [Key]
    public int id { get; set; }
    
    [Column(TypeName = "nvarchar(50)")]
    public string? StatusName { get; set; }

    public ICollection<Product>? Products { get; set; }
}