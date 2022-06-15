using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;

namespace CarvedRock.Admin.Controllers;

public class ProductsController : Controller
{
    private readonly IProductLogic _logic;
    public readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductLogic logic, ILogger<ProductsController> logger)
    {
        this._logic = logic;
        _logger = logger;
    }


    public async Task<IActionResult> Create()
    {
        var model = await _logic.InitializeProductModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductModel product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }
        try
        {
            await _logic.AddNewProduct(product);
            return RedirectToAction(nameof(Index));
        }
        catch (ValidationException valEx)
        {
            var results = new ValidationResult(valEx.Errors);
            results.AddToModelState(ModelState, null);
            await _logic.GetAvailableCategories(product);
            return View(product);
        }
    }
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return View("NotFound");

        var productModel = await _logic.GetProductById(id.Value);

        if (productModel == null) return View("NotFound");

        return View(productModel);
    }
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _logic.RemoveProduct(id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _logic.GetProductById(id);
        if (product == null)
        {
            _logger.LogInformation("Details not found for id {id}", id);
            return View("NotFound");
        }
        else
        {
            return View(product);
        }
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            _logger.LogInformation("Details not found for id {id}", id);
            return View("NotFound");
        }

        var productModel = await _logic.GetProductById(id.Value);

        if (productModel == null)
        {
            _logger.LogInformation("Details not found for id {id}", id);
            return View("NotFound");
        }
        await _logic.GetAvailableCategories(productModel);
        return View(productModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id,Name,Description,Price,IsActive,CategoryId")] ProductModel product)
    {
        if (id != product.Id)
        {
            _logger.LogInformation("Details not found for id {id}", id);
            return View("NotFound");
        }


        if (ModelState.IsValid)
        {
            await _logic.UpdateProduct(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);

    }

    public async Task<IActionResult> Index()
    {
        var products = await _logic.GetAllProducts();
        return View(products);
    }

}
