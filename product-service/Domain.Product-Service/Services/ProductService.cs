
public class ProductService : IProductService {
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductModel> CreateProduct(ProductModel product)
    {
        var prod = await _productRepository.CreateProduct(new ProductEntity() {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CategoryId = product.CategoryId,
        });

        return new ProductModel() {
            Id = prod.Id,
            Name = prod.Name,
            Description = prod.Description,
            Price = prod.Price,
            Stock = prod.Stock,
            CategoryId = prod.CategoryId,
        };
    }

    public void DeleteProduct(int id)
    {
        _productRepository.DeleteProduct(id);
    }

    public async Task<ProductModel> GetProductById(int id)
    {
        var prod = await _productRepository.GetProductById(id);
        
        if (prod == null) return null;

        return new ProductModel() {
            Id = prod.Id,
            Name = prod.Name,
            Description = prod.Description,
            Price = prod.Price,
            Stock = prod.Stock,
            CategoryId = prod.CategoryId
        };
    }

    public async Task<IEnumerable<ProductModel>> GetProducts()
    {
        var prod = await _productRepository.GetProducts();
        return prod.Select(p => new ProductModel() {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Stock = p.Stock,
            CategoryId = p.CategoryId
        });
    }

    public async Task<ProductModel> UpdateProduct(ProductModel product, int id)
    {
        var prod = await _productRepository.GetProductById(id);
        prod.Name = product.Name;
        prod.Description = product.Description;
        prod.Price = product.Price;
        prod.Stock = product.Stock;
        prod.CategoryId = product.CategoryId;
        var changed = await _productRepository.UpdateProduct(prod);
        return new ProductModel() {
            Id = changed.Id,
            Name = changed.Name,
            Description = changed.Description,
            Price = changed.Price,
            Stock = changed.Stock,
            CategoryId = changed.CategoryId
        };
    }
}