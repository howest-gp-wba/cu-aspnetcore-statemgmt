var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
//// Adds a default in-memory implementation of IDistributedCache.
//services.AddDistributedMemoryCache();

// Add a default in-memory implementation of a Cache
//services.AddMemoryCache();

// Add Session service to the application
builder.Services.AddSession(options =>
{
    //options.Cookie.SameSite = SameSiteMode.Strict; //protect session id from being hijacked
    options.Cookie.HttpOnly = true;
    options.IdleTimeout = TimeSpan.FromSeconds(15); // Set a short timeout for easy testing.
});


builder.Services.AddControllersWithViews();

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

//enable session for the application
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Movies}/{action=ShowMovies}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();