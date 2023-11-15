using System.ComponentModel.DataAnnotations;
using WebChild.Data;

namespace WebChild.Models;

public class Cart
{
    [Key]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public User? User { get; set; }

    public ICollection<CartDetail>? CartDetails { get; set; }
}