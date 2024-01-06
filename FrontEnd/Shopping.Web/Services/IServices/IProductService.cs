namespace Shopping.Web;

public interface IProductService:IBaseService
{
    Task<T> GetAllProductAync<T>();
    Task<T> GetProductByIdAsync<T>(int ProductId);
    Task<T> CreateProductAsync<T>(AddProductDto addProductDto);
    Task<T> UpdateProductAsync<T>(EditProductDto editProduct);
    Task<T> DeleteProductAsync<T>(int ProductId);
}
