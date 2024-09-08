
public interface IProductService {
    Task<IEnumerable<ProductModel>> GetProducts();
    Task<ProductModel> GetProductById(int id);
    Task<ProductModel> CreateProduct(ProductModel product);
    Task<ProductModel> UpdateProduct(ProductModel product, int id);
    void DeleteProduct(int id);
}