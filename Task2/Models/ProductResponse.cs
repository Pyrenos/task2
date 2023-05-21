using System.Text.Json.Serialization;

namespace Task2.Models;

public class ProductResponse
{
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
    [JsonPropertyName("products")]
    public List<Product> Products { get; set; }
}