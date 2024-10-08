namespace CarvedRock.Admin.Domain.Data;

public class CarvedRockRepository : ICarvedRockRepository
{
    private readonly ProductContext _context;

    public CarvedRockRepository(ProductContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products
            .Include(i => i.Category)
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await _context.Products
            .Include(i => i.Category)
            .FirstOrDefaultAsync(m => m.Id == productId);
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product; // will have updated ID value
    }

    public async Task UpdateProductAsync(Product product)
    {
        try
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (_context.Products.Any(e => e.Id == product.Id))
            {
                // product exists and update exception is real
                throw;
            }
            // caught and swallowed exception can occur if 
            // the other update was a delete
        }
    }

    public async Task RemoveProductAsync(int productIdToRemove)
    {
        var product = await _context.Products
                        .FirstOrDefaultAsync(p => p.Id == productIdToRemove);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category> GetCategoryByIdAsync(int categoryId)
    {
        return await _context.Categories.Where(w => w.Id == categoryId).FirstOrDefaultAsync() ?? new Category() { Id = 0, Name = "None" };
    }

    public async Task<Category> AddCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category; // will have updated ID value
    }

    public async Task RemoveCategoryAsync(int id)
    {
        var category = await _context.Categories
                        .FirstOrDefaultAsync(p => p.Id == id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateCategoryAsync(Category categoryToSave)
    {
        try
        {
            _context.Update(categoryToSave);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (_context.Categories.Any(e => e.Id == categoryToSave.Id))
            {
                // product exists and update exception is real
                throw;
            }
            // caught and swallowed exception can occur if 
            // the other update was a delete
        }
    }
}