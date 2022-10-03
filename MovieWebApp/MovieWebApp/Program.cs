using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MovieWebApp.Service;
using MovieWebApp.Utility.Extension;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
AppSettings.Host = builder.Configuration["MovieAPI:Host"];
AppSettings.TimeOut = builder.Configuration["MovieAPI:TimeOut"];
AppSettings.SecretKey = builder.Configuration["AppSettings:SecretKey"];
#region Add services to the container
// Add razorpage services 
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//Add JWT services 
var secretKeyByte = Encoding.UTF8.GetBytes(AppSettings.SecretKey);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.SaveToken = true;
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKeyByte),
            ClockSkew = TimeSpan.Zero
        };
    });
// Add HttpClient services
builder.Services.AddHttpClient("api", c =>
{
    c.BaseAddress = new Uri(AppSettings.Host);
});

builder.Services.AddSingleton<UserServices>();

#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.Use(async (context, next) =>
{
    var result = context.Request.Cookies.TryGetValue("accessToken", out string accessToken);
    if (result)
    {
        context.Request.Headers.Authorization = "Bearer " + accessToken; 
    }
    await next();
});

app.UseRouting();

// for status code error
app.UseStatusCodePagesWithRedirects("/ErrorPage/Error{0}");

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapBlazorHub();

app.Run();
