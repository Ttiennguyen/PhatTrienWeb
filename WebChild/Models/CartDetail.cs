using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebChild.Data;

namespace WebChild.Models;

public class CartDetail
{
    [Key]
    public int Id { get; set; }

    public int CartId { get; set; }
    public Cart? Cart { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public int? CartDetailAmount { get; set; }

    [Column(TypeName = "float")]
    public float? CartDetailTotal { get; set; }
}