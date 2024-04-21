namespace ECommerce.Core.Models;

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; } 

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }
}
