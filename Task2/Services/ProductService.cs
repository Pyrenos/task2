using System.Text.Json;
using Task2.Models;

namespace Task2.Services;

public class ProductService : IProductService
{
    public List<Product>? GetProductData()
    {
        string fileContent = File.ReadAllText("./Data/warehouseproducts.json");
        return JsonSerializer.Deserialize<List<Product>>(fileContent);
    }
}