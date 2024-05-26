using System.ComponentModel.DataAnnotations;

namespace Product.Models;

public class testproduct
{
    public int Id { get; set; }
    public string? ProductName { get; set; }
    public string? ProductType { get; set; }
    public decimal Price { get; set; }
}