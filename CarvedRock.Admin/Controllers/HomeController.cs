using CarvedRock.Admin.Domain.Models;
using System.Diagnostics;

namespace CarvedRock.Admin.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _env;

    public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public IActionResult Index()
    {
        var filePath = Path.Combine(_env.WebRootPath, "kitchen_sink.html");
        var htmlContent = System.IO.File.ReadAllText(filePath);
        return View((object)htmlContent);
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
