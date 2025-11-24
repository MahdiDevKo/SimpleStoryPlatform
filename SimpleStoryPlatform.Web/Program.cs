using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SimpleStoryPlatform.Web;
using SimpleStoryPlatform.Web.Components;
using SimpleStoryPlatform.Web.MiddleWares;
using SimpleStoryPlatform.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//builder.Services.AddHttpClient("defHttp", (sp, client) =>
//{
//    var config = sp.GetRequiredService<IConfiguration>();
//    var baseUrl = config["ApiBaseUrl"];
//    client.BaseAddress = new Uri(baseUrl);
//});
builder.Services.AddHttpContextAccessor();


builder.Services.AddTransient<TokenChecker>();

builder.Services.AddScoped<LittleMessagerService>();

builder.Services.AddHttpClient("ApiClient", (sp, client) =>
{
    client.BaseAddress = new Uri(sp.GetRequiredService<IConfiguration>()["ApiBaseUrl"]);
})
.AddHttpMessageHandler<TokenChecker>();

//builder.Services.AddScoped<IHttpContextAccessor ,HttpContextAccessor>();

builder.Services.AddHttpClient("defClient");

builder.Services.AddScoped(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    return factory.CreateClient("ApiClient");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
