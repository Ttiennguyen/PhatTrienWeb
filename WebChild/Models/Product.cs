using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebChild.Data;
using WebChild.Models;

namespace WebChild.Data;

public class Product
{
    [Key]
    public int Id { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string? ProductName { get; set; }

    [Column(TypeName = "float")]
    public float? ProductPrice { get; set; }
    
    [Column(TypeName = "float")]
    public float? ProductPriceSale { get; set; }

    public int? ProductAmount { get; set; }
    
    [DisplayFormat(DataFormatString = "{0:MMM-dd-yy}")]
    public DateTime? DateInclude { get; set; }

    [Column(TypeName = "nvarchar(50)")]
    public string? Brand { get; set; }

    [Column(TypeName = "nvarchar(200)")]
    public string? image { get; set; }
    
    public int StatusId { get; set; }
    public Status? Status { get; set; }
    
    [Column(TypeName = "nvarchar(500)")]
    public string? ProductDescription { get; set; }
    
    public ICollection<Order>? CartDetails;
    public ICollection<Product_Order>? Product_Orders;
}