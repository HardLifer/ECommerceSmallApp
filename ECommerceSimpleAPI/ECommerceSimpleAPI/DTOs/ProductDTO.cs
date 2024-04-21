namespace ECommerceSimpleAPI.DTOs;

public record class ProductDTO
{
    public string Name { get; init; } = null!;

    public string? Description { get; init; }

    public decimal Price { get; init; }

    public int Quantity { get; init; }
}
