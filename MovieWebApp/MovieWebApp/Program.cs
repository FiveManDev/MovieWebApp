using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
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
//Add services JWT, Google and Facebook 
var secretKeyByte = Encoding.UTF8.GetBytes(AppSettings.SecretKey);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/login";
})
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
    })
    .AddGoogle(options =>
    {
        IConfigurationSection googleAuthNSection =
        builder.Configuration.GetSection("Authentication:Google");
        options.ClientId = googleAuthNSection["ClientId"];
        options.ClientSecret = googleAuthNSection["ClientSecret"];
    })
   .AddFacebook(options =>
   {
       IConfigurationSection facebookAuthNSection =
       builder.Configuration.GetSection("Authentication:Facebook");
       options.ClientId = facebookAuthNSection["ClientId"];
       options.ClientSecret = facebookAuthNSection["ClientSecret"];
   });
// Add HttpClient services
builder.Services.AddHttpClient("api", c =>
{
    c.BaseAddress = new Uri(AppSettings.Host);
});
builder.Services.AddSingleton<UserServices>();
builder.Services.AddSingleton<ProfileServices>();
builder.Services.AddSingleton<MovieServices>();
builder.Services.AddSingleton<ReviewServices>();
builder.Services.AddSingleton<GenreServices>();
builder.Services.AddSingleton<ClassificationServices>();
//Add services Momo
AppSettings.PartnerCode = builder.Configuration["MomoConnectionInformation:PartnerCode"];
AppSettings.MomoAccessKey = builder.Configuration["MomoConnectionInformation:AccessKey"];
AppSettings.MomoSerectkey = builder.Configuration["MomoConnectionInformation:Serectkey"];
AppSettings.Endpoint = builder.Configuration["MomoConnectionInformation:Endpoint"];
AppSettings.ReturnUrl = builder.Configuration["MomoConnectionInformation:ReturnUrl"];
AppSettings.NotifyUrl = builder.Configuration["MomoConnectionInformation:Notifyurl"];
#endregion
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
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

app.UseRouting();

app.Use(async (context, next) =>
{
    var result = context.Request.Cookies.TryGetValue("accessToken", out string accessToken);
    if (result)
    {
        context.Request.Headers.Authorization = "Bearer " + accessToken;
    }
    await next();
});

// for status code error
app.UseStatusCodePagesWithRedirects("/ErrorPage?statusCode={0}");

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
