using Task2.Models;

namespace Task2.Services;

public interface IProductService
{
    List<Product>? GetProductData();
}