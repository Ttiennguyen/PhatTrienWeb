using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebChild.Data;

namespace WebChild.Models;

public class Product_Order
{
    [Key]
    public int Id { get; set; }
    
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    
    [Column(TypeName = "int")]
    public int Quanlity { get; set; }
    
    [Column(TypeName = "float")]
    public float? Price { get; set; }
}