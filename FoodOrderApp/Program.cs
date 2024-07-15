using FoodOrderApp;
using FoodOrderApp.Components;
using FoodOrderApp.Interop.TeamsSDK;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Graph;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var config = builder.Configuration.Get<ConfigOptions>();
builder.Services.AddTeamsFx(config.TeamsFx.Authentication);
builder.Services.AddScoped<MicrosoftTeams>();
builder.Services.AddFluentUIComponents();
builder.Services.AddControllers();
//builder.Services.AddHttpClient("WebClient", client => client.Timeout = TimeSpan.FromSeconds(600));
builder.Services.AddHttpClient("FoodOrderApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7230/");
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddAntiforgery(o => o.SuppressXFrameOptionsHeader = true);

/*builder.Services.AddScoped<GraphServiceClient>(sp =>
{
    var tokenAcquisition = sp.GetRequiredService<ITokenAcquisition>();
    return new GraphServiceClient(new DelegateAuthenticationProvider(async (requestMessage) =>
    {
        var token = await tokenAcquisition.GetAccessTokenForUserAsync(new[] { "User.Read" });
        requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }));
});*/

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

app.UseStaticFiles();

app.UseRouting();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

public class GraphClientHandler : DelegatingHandler
{
    private readonly IAccessTokenProvider _tokenProvider;

    public GraphClientHandler(IAccessTokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var result = await _tokenProvider.RequestAccessToken();
        if (result.TryGetToken(out var token))
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Value);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}