using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebChild.Data;

namespace WebChild.Models;

public class BillDetail
{
    [Key]
    public int Id { get; set; }

    public int BillId { get; set; }
    public Bill? Bill { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }
    
    public int? BillDetailAmount { get; set; }

    [Column(TypeName = "float")]
    public float? BillDetailTotal { get; set; }
}