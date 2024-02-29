using Shopping.Web.Models.Dtos;

namespace Shopping.Web.Services
{
    public class CartService:BaseService,ICartService
    {
        private readonly IHttpClientFactory httpFactory;
        public CartService(IHttpClientFactory httpClient) : base(httpClient)
        {
            this.httpFactory = httpClient;
        }

        public async Task<T> GetCartByUserIdAsync<T>(string userId, string token = null)
        {
            return await this.SendAync<T>(new ApiRequest
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ShoppingCartApiBase+ "/api/cart/GetCart/"+userId,
                AccessToken = token

            });
        }

        public async Task<T> AddToCartAsync<T>(CartDto cartDto, string token = null)
        {
            return await this.SendAync<T>(new ApiRequest
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartApiBase+ "/api/cart/AddCart",
                AccessToken = token
            });
        }

        public async Task<T> UpdateToCartAsync<T>(CartDto cartDto, string token = null)
        {
            return await this.SendAync<T>(new ApiRequest
            {
                ApiType = ApiType.PUT,
                Data = cartDto,
                Url = SD.ShoppingCartApiBase + "/api/cart/Update",
                AccessToken = token
            });
        }

        public async Task<T> RemoveFromCartAsync<T>(int cartId, string token = null)
        {
            return await this.SendAync<T>(new ApiRequest
            {
                ApiType = SD.ApiType.DELETE,
                Data = cartId,
                Url = SD.ShoppingCartApiBase + "/api/cart/RemoveCart",
                AccessToken = token
            });
        }
    }
}
