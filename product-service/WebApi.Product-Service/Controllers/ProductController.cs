
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductController {
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IEnumerable<ProductViewModel>> GetProducts()
    {
        var products = await _productService.GetProducts();
        return products.Select(p => new ProductViewModel
        {
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Stock = p.Stock,
            CategoryId = p.CategoryId
        });
    }

    [HttpGet("{id}")]
    public async Task<ProductViewModel> GetProductById(int id)
    {
        var product = await _productService.GetProductById(id);
        
        if (product == null) return null;

        return new ProductViewModel
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CategoryId = product.CategoryId
        };
    }

    [HttpPost]
    public async Task<ProductViewModel> CreateProduct([FromBody] ProductViewModel product)
    {
        var productModel = new ProductModel
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CategoryId = product.CategoryId
        };

        var createdProduct = await _productService.CreateProduct(productModel);
        return new ProductViewModel
        {
            Name = createdProduct.Name,
            Description = createdProduct.Description,
            Price = createdProduct.Price,
            Stock = createdProduct.Stock,
            CategoryId = createdProduct.CategoryId
        };
    }

    [HttpPut("{id}")]
    public async Task<ProductViewModel> UpdateProduct([FromBody] ProductViewModel product, int id)
    {
        var productModel = new ProductModel
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CategoryId = product.CategoryId
        };

        var updatedProduct = await _productService.UpdateProduct(productModel, id);
        return new ProductViewModel
        {
            Name = updatedProduct.Name,
            Description = updatedProduct.Description,
            Price = updatedProduct.Price,
            Stock = updatedProduct.Stock,
            CategoryId = updatedProduct.CategoryId
        };
    }

    [HttpDelete("{id}")]
    public void DeleteProduct(int id)
    {
        _productService.DeleteProduct(id);
    }

    [HttpGet("users")]
    public async Task<List<UserEntity>> GetUsers()
    {
        var users = await _productService.GetUsers();
        return users;
    }

}