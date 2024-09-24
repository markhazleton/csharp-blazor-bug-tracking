using CarvedRock.Admin.Domain.Data;
using CarvedRock.Admin.Domain.Logic;
using CarvedRock.Admin.Domain.Models;
using FluentValidation;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();
builder.Services.AddDbContext<ProductContext>();
builder.Services.AddScoped<ICarvedRockRepository, CarvedRockRepository>();
builder.Services.AddScoped<IProductLogic, ProductLogic>();
builder.Services.AddScoped<ICategoryLogic, CategoryLogic>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarvedRock API", Description = "CarvedRock Product Catalog", Version = "v1" });
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarvedRock API V1");
});

using var context = new ProductContext(builder.Configuration);
var repo = new CarvedRockRepository(context);
var val = new ProductValidator(repo);
var productLogic = new ProductLogic(repo, val);


app.MapGet("api/products/{id}", async (int id) => await productLogic.GetProductById(id)
    is ProductModel product ? Results.Ok(product) : Results.NotFound())
    .Produces<CategoryModel>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);
app.MapGet("/api/products", async () => await productLogic.GetAllProducts())
    .Produces<List<ProductModel>>(StatusCodes.Status200OK);
app.MapPost("/api/products", async (ProductModel product) => await productLogic.AddNewProduct(product))
    .Produces<CategoryModel>(StatusCodes.Status201Created);

app.MapPut("/api/products", async (ProductModel product) => await productLogic.UpdateProduct(product))
    .Produces<CategoryModel>(StatusCodes.Status202Accepted);

var categoryLogic = new CategoryLogic(repo);
app.MapGet("api/categories/{id}", async (int id) => await categoryLogic.GetCategoryById(id))
    .Produces<CategoryModel>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);
app.MapGet("api/categories", () => categoryLogic.GetAllCategories());
app.MapPost("api/categories", (CategoryModel cat) => categoryLogic.AddNewCategory(cat));
app.MapPut("api/categories", (CategoryModel cat) => categoryLogic.UpdateCategory(cat));


app.Run();
