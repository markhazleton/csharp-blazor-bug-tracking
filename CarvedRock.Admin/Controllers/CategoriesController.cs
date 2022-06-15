
namespace CarvedRock.Admin.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryLogic _logic;
    public readonly ILogger<CategoriesController> _logger;

    public CategoriesController(ICategoryLogic logic, ILogger<CategoriesController> logger)
    {
        _logic = logic;
        _logger = logger;
    }

    private async Task<bool> CategoryExists(int id)
    {
        var lookupCategory = await _logic.GetCategoryById(id);
        return lookupCategory != null ? true : false;
    }

    // GET: Categories/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Categories/Create
    // To protect from over posting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryModel category)
    {
        if (ModelState.IsValid)
        {
            category = await _logic.AddNewCategory(category);
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    // GET: Categories/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        var category = await _logic.GetCategoryById(id);
        if (category == null)
        {
            return View("NotFound");
        }
        return View(category);
    }

    // POST: Categories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _logic.RemoveCategory(id);
        return RedirectToAction(nameof(Index));
    }

    // GET: Categories/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        var category = await _logic.GetCategoryById(id);

        if (category == null)
        {
            return View("NotFound");
        }
        return View(category);
    }

    // GET: Categories/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        var category = await _logic.GetCategoryById(id);
        if (category == null)
        {
            return View("NotFound");
        }
        return View(category);
    }

    // POST: Categories/Edit/5
    // To protect from over posting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CategoryModel category)
    {
        if (id != category.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _logic.UpdateCategory(category);
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }
    // GET: Categories
    public async Task<IActionResult> Index()
    {
        return View(await _logic.GetAllCategories());
    }
}
