using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Fumble.FrontEnd.ViewModels;
using Fumble.FrontEnd.Infra.ApiClients.Catalog;

namespace Fumble.FrontEnd.Controllers;

public class CatalogController : Controller
{
    private readonly ILogger<CatalogController> _logger;

    public CatalogController(ILogger<CatalogController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index([FromServices] ICatalogApiClient catalogApi)
    {
        var e = catalogApi.GetProducts(take: 15, skip: 0);

        return View();
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
}
