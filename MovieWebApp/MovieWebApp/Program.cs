using MovieWebApp.Utility.Extension;

var builder = WebApplication.CreateBuilder(args);
AppSettings.Host = builder.Configuration["MovieAPI:Host"];
AppSettings.TimeOut = builder.Configuration["MovieAPI:TimeOut"];
// Add services to the container.
builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// Delete this when starting project
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");
//////////////////////////////////////////////
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
