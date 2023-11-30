using System.ComponentModel.DataAnnotations;
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
    
    public int Quanlity { get; set; }
    public double Price { get; set; }
}