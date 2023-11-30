using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebChild.Data;

namespace WebChild.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }
    
    [StringLength(450)]
    public DateTime? CreatedDate { get; set; }
    public int QuanlityTotal { get; set; }
    public int Total_Price { get; set; }
    public DateTime? ShippingDate { get; set; }
    public string? Shipping_Address { get; set; }
    public string? Email_User { get; set; }
    public string? Status { get; set; }

    public ICollection<Product_Order> Product_Orders { get; set; }

}