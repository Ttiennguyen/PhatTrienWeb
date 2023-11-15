using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebChild.Data;

namespace WebChild.Models;

public class Bill
{
    [Key]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public User? User { get; set; }

    [DisplayFormat(DataFormatString = "{0:MMM-dd-yy}")]
    public DateTime? DateOfPurchase { get; set; }

    [Column(TypeName = "nvarchar(200)")]
    public string? DeliveryAddress { get; set; }
    
    [Column(TypeName = "nvarchar(13)")]
    public string? DeliveryPhoneNumber { get; set; }
    
    public int? Status { get; set; }

    public ICollection<BillDetail>? BillDetails { get; set; }
}
