using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebChild.Models;

namespace WebChild.Data;

public class User
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "char(50)")]
    public string UserName { get; set; }
    
    [Column(TypeName = "char(64)")]
    public string Password { get; set; }

    [Column(TypeName = "nvarchar(90)")]
    public string? FullName { get; set; }
    
    [Column(TypeName = "nvarchar(13)")]
    public string? PhoneNumber { get; set; }

    public int? Role { get; set; }

    public ICollection<Cart>? Carts { get; set; }
}