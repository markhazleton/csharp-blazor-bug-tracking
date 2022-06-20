using CarvedRock.Admin.Areas.Identity.Data;
using CarvedRock.Admin.Data;
using CarvedRock.Domain.Data;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdminContext>(); ;

builder.Services.AddDefaultIdentity<AdminUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AdminContext>(); ;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();
builder.Services.AddDbContext<ProductContext>();
builder.Services.AddScoped<ICarvedRockRepository, CarvedRockRepository>();
builder.Services.AddScoped<IProductLogic, ProductLogic>();
builder.Services.AddScoped<ICategoryLogic, CategoryLogic>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var ctx = services.GetRequiredService<ProductContext>();
    ctx.Database.Migrate();

    var userCtx = services.GetRequiredService<AdminContext>();
    userCtx.Database.Migrate();

    if (app.Environment.IsDevelopment())
    {
        ctx.SeedInitialData();
    }
}

app.UseExceptionHandler("/Home/Error");
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); ;
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
