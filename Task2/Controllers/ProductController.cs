using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Task2.Models;
using Task2.Services;

namespace Task2.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMemoryCache _memoryCache;
    private const string productCacheKey = "PRODUCTS";
    private List<Product> Products { 
        get {
            List<Product> products = null;
            bool productCacheFound = _memoryCache.TryGetValue(productCacheKey, out products);
            if (productCacheFound)
            {
                return products;
            }
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromDays(1));
            products = _productService.GetProductData();
            _memoryCache.Set(productCacheKey, products, cacheEntryOptions);
            return products;
        }
    }

    public ProductController(IProductService productService, IMemoryCache memoryCache)
    {
        _productService = productService;
        _memoryCache = memoryCache;
    }
    
    [HttpGet("GetAvailableProductsByType/{productType}")]
    public ProductResponse GetAvailableProductsByType(string productType)
    {
        List<Product> products = Products.Where(p => p.ProductType == productType && p.IsAvailable)
            .OrderBy(p => p.StorageDate).Take(10).ToList();
        return new ProductResponse()
        {
            Quantity = products.Count,
            Products = products
        };
    }
    
    [HttpPost("PackageAvailableProductsByType")]
    public PackageResponse PackageAvailableProductsByType(HashSet<string> _serialsNumbers)
    {
        Dictionary<string, Product> products = Products.Where(p =>  _serialsNumbers.Contains(p.SerialNumber) && p.IsAvailable)
            .OrderBy(p => p.StorageDate)
            .Take(10)
            .ToDictionary(p => p.SerialNumber);
        foreach (Product p in Products)
        {
            p.IsAvailable = false;
        }
        return new PackageResponse()
        {
            AvailableQuantity = products.Values.Count,
            PackedProducts = products.Values.ToList(),
            NotPackedProducts = _serialsNumbers.Where(sn => !products.ContainsKey(sn)).ToList()
        };
    }
    [HttpPost("PackageAvailableProductByTypeAndQuantity")]
    public ProductResponse PackageAvailableProductByTypeAndQuantity(string productType, int quantity)
    {
        List<Product> products = Products.Where(p => p.ProductType == productType).OrderBy(p => p.StorageDate)
            .Take(quantity).ToList();
        return new ProductResponse()
        {
            Quantity = products.Count,
            Products = products
        };
    }
}