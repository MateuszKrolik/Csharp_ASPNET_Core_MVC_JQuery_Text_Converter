using Microsoft.EntityFrameworkCore;
using TextConverterApp.Data;
using TextConverterApp.RequestHelpers;
using TextConverterApp.Services.TextConverterService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<SessionHelper>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ConversionsDbContext>(option =>
    {
        option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
);
builder.Services.AddHttpClient<ITextConverterService, TextConverterServiceImpl>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TextConverter}/{action=Index}/{id?}");

app.Run();