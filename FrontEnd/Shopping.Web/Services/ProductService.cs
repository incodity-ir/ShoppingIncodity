namespace Shopping.Web.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IHttpClientFactory clientFactory;
        public ProductService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            this.clientFactory = clientFactory;
        }
        public async Task<T> CreateProductAsync<T>(AddProductDto addProductDto, string token)
        {
            return await this.SendAync<T>(new ApiRequest()
            {
                ApiType = ApiType.POST,
                Data = addProductDto,
                Url = ProductAPIBase + "/api/product/create",
                AccessToken = token

            });
        }

        public async Task<T> DeleteProductAsync<T>(int ProductId, string token)
        {
            return await this.SendAync<T>(new ApiRequest()
            {
                ApiType = ApiType.DELETE,
                Url = ProductAPIBase + "/api/product/delete/" + ProductId,
                AccessToken = token
            });
        }

        public async Task<T> GetAllProductAync<T>(string token)
        {
            return await this.SendAync<T>(new ApiRequest()
            {
                ApiType = ApiType.GET,
                Url = ProductAPIBase + "/api/product/getall",
                AccessToken = token
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int ProductId, string token)
        {
            return await this.SendAync<T>(new ApiRequest()
            {
                ApiType = ApiType.GET,
                Url = ProductAPIBase + "/api/product/GetBYID/" + ProductId,
                AccessToken = token
            });
        }

        public async Task<T> UpdateProductAsync<T>(EditProductDto editProduct, string token)
        {
            return await this.SendAync<T>(new ApiRequest()
            {
                ApiType = ApiType.PUT,
                Data = editProduct,
                Url = ProductAPIBase + "/api/product/update",
                AccessToken = token

            });
        }

    }
}