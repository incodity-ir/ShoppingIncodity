namespace Shopping.Web.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IHttpClientFactory clientFactory;
        public ProductService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            this.clientFactory = clientFactory;
        }
        public async Task<T> CreateProductAsync<T>(AddProductDto addProductDto)
        {
            return await this.SendAync<T>(new ApiRequest()
            {
                ApiType = ApiType.POST,
                Data = addProductDto,
                Url = ProductAPIBase + "/api/product/create",
                AccessToken = ""

            });
        }

        public async Task<T> DeleteProductAsync<T>(int ProductId)
        {
            return await this.SendAync<T>(new ApiRequest()
            {
                ApiType = ApiType.DELETE,
                Url = ProductAPIBase + "api/product/delete/" + ProductId,
                AccessToken = ""
            });
        }

        public async Task<T> GetAllProductAync<T>()
        {
            return await this.SendAync<T>(new ApiRequest()
            {
                ApiType = ApiType.GET,
                Url = ProductAPIBase + "/api/product/getall",
                AccessToken = ""
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int ProductId)
        {
            return await this.SendAync<T>(new ApiRequest()
            {
                ApiType = ApiType.GET,
                Url = ProductAPIBase + "/api/product/GetBYID/" + ProductId,
                AccessToken = ""
            });
        }

        public async Task<T> UpdateProductAsync<T>(EditProductDto editProduct)
        {
            return await this.SendAync<T>(new ApiRequest()
            {
                ApiType = ApiType.POST,
                Data = editProduct,
                Url = ProductAPIBase + "/api/product/update",
                AccessToken = ""

            });
        }

    }
}