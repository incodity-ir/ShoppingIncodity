using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Web.Models;

namespace Shopping.Web.Controllers;

public class HomeController : Controller
{
    #region Fileds

    private readonly ILogger<HomeController> _logger;
    private readonly IProductService productService;


    public HomeController(ILogger<HomeController> logger, IProductService productService)
    {
        _logger = logger;
        this.productService = productService;
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
