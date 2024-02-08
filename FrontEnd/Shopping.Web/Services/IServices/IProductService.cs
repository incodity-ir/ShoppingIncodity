namespace Shopping.Web;

public interface IProductService:IBaseService
{
    Task<T> GetAllProductAync<T>(string token);
    Task<T> GetProductByIdAsync<T>(int ProductId,string token);
    Task<T> CreateProductAsync<T>(AddProductDto addProductDto, string token);
    Task<T> UpdateProductAsync<T>(EditProductDto editProduct, string token);
    Task<T> DeleteProductAsync<T>(int ProductId, string token);
}
