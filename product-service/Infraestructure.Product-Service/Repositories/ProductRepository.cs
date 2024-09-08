
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepository
{

    private readonly PostgresContext _context;

    public ProductRepository(PostgresContext context)
    {
        _context = context;
    }

    public async Task<ProductEntity> CreateProduct(ProductEntity product)
    {
        product.CreatedAt = DateTime.UtcNow;
        product.UpdatedAt = DateTime.UtcNow;
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public void DeleteProduct(int id)
    {
        _context.Products.Remove(_context.Products.Find(id));
        _context.SaveChanges();
    }

    public async Task<ProductEntity> GetProductById(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<IEnumerable<ProductEntity>> GetProducts()
    {
        return _context.Products.ToList();
    }

    public Task<ProductEntity> UpdateProduct(ProductEntity product)
    {
        product.UpdatedAt = DateTime.UtcNow;
        _context.Entry(product).State = EntityState.Modified;
        _context.SaveChanges();
        return Task.FromResult(product);
    }
}