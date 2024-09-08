
public interface IProductRepository {
    Task<IEnumerable<ProductEntity>> GetProducts();
    Task<ProductEntity> GetProductById(int id);
    Task<ProductEntity> CreateProduct(ProductEntity product);
    Task<ProductEntity> UpdateProduct(ProductEntity product);
    void DeleteProduct(int id);
}