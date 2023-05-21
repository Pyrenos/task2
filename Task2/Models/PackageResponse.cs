using System.Text.Json.Serialization;

namespace Task2.Models;

public class PackageResponse
{
    [JsonPropertyName("availableQuantity")]
    public int AvailableQuantity { get; set; }
    [JsonPropertyName("packedProducts")]
    public List<Product> PackedProducts { get; set; }
    
    [JsonPropertyName("notPackedProducts")]
    public List<string> NotPackedProducts { get; set; }
}