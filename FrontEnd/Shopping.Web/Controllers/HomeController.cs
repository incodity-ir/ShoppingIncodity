using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Web.Models;
using Shopping.Web.Models.Dtos;

namespace Shopping.Web.Controllers;

public class HomeController : Controller
{
    #region Fileds

    private readonly ILogger<HomeController> _logger;
    private readonly IProductService productService;
    private readonly ICartService cartService;

    public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
    {
        _logger = logger;
        this.productService = productService;
        this.cartService = cartService;
    }

    #endregion


    public async Task<IActionResult> Index()
    {
        List<ProductDto> products = new();
        var response = await productService.GetAllProductAync<ResponseDto>("");
        if (response != null && response.IsSuccess)
            products = JsonConvert.DeserializeObject<List<ProductDto>>(response.Result.ToString());
        return View(products);
    }

    
    public async Task<IActionResult> Detail(int productId)
    {
        ProductDto product = new();
        var response = await productService.GetProductByIdAsync<ResponseDto>(productId, "");
        if (response != null && response.IsSuccess)
            product = JsonConvert.DeserializeObject<ProductDto>(response.Result.ToString());
        return View("Detail",product);
    }

    [HttpPost]
    [ActionName("Detail")]
    [Authorize]
    public async Task<IActionResult> DetailPost(ProductDto productDto)
    {
        CartDto cart = new()
        {
            CartHeader = new(){UserId = HttpContext.GetUserId()}
        };

        CartDetailDto cartDetail = new()
        {
            Count = productDto.Count,
            ProductId = productDto.ProductId
        };

        var response = await productService.GetProductByIdAsync<ResponseDto>(productDto.ProductId, "");
        if (response != null && response.IsSuccess)
        {
            cartDetail.Product = JsonConvert.DeserializeObject<ProductDto>(response.Result.ToString());
        }

        List<CartDetailDto> cartDetails = new();
        cartDetails.Add(cartDetail);
        cart.CartDetails = cartDetails;

        var accessToken = await HttpContext.GetToken();
        var AddToCartResponse = await cartService.AddToCartAsync<ResponseDto>(cart, accessToken);
        if (AddToCartResponse != null && AddToCartResponse.IsSuccess)
        {
            return RedirectToAction(nameof(Index));
        }
        return View("Detail", productDto);
    }



    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Authorize]
    public async Task<IActionResult> Login()

    {
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Logout()
    {
        return SignOut("Cookies", "oidc");
    }
}
