using FoodOrderApp;
using FoodOrderApp.Components;
using FoodOrderApp.Interop.TeamsSDK;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.TeamsFx;
using Microsoft.Graph.Models.ExternalConnectors;
using BlazorSample;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals;
    });
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMsalAuthentication(options =>
{
    var azureAd = builder.Configuration.GetSection("AzureAd");
    options.ProviderOptions.Authentication.Authority = $"{azureAd["Instance"]}{azureAd["TenantId"]}";
    options.ProviderOptions.Authentication.ClientId = azureAd["ClientId"];
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/.default");
});
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

var config = builder.Configuration.Get<ConfigOptions>();
builder.Services.AddTeamsFx(config.TeamsFx.Authentication);

builder.Services.AddScoped<MicrosoftTeams>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<ToastService>();
builder.Services.AddFluentUIComponents();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddMicrosoftIdentityConsentHandler();
builder.Services.AddServerSideBlazor()
    .AddMicrosoftIdentityConsentHandler();
builder.Services.AddControllers();
//builder.Services.AddHttpClient("WebClient", client => client.Timeout = TimeSpan.FromSeconds(600));
builder.Services.AddHttpClient("FoodOrderApi", client =>
{
    client.BaseAddress = new Uri("https://foodorderapi20240728140851.azurewebsites.net/");
    //client.BaseAddress = new Uri("https://localhost:7230/");
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddAntiforgery(o => o.SuppressXFrameOptionsHeader = true);

var baseUrl = builder.Configuration.GetSection("MicrosoftGraph")["BaseUrl"];
var scopes = builder.Configuration.GetSection("MicrosoftGraph:Scopes")
    .Get<List<string>>();

builder.Services.AddGraphClient(baseUrl, scopes);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
