using System.ComponentModel.DataAnnotations;
using WebChild.Data;

namespace WebChild.Models;

public class CartItem
{
    public int quantity { set; get; }
    public Product? product { set; get; }
}