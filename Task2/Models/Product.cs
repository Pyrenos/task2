using System.Text.Json.Serialization;

namespace Task2.Models;

public class Product
{
    [JsonPropertyName("serialNumber")]
    public string SerialNumber { get; set; } = String.Empty;
    [JsonPropertyName("storageDate")]
    public DateOnly StorageDate { get; set; }
    [JsonPropertyName("productType")]
    public string ProductType { get; set; } = string.Empty;
    [JsonPropertyName("isAvailable")]
    public bool IsAvailable { get; set; }
}