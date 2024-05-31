using AgriEnergyConnect.Application;
using AgriEnergyConnect.Infrastructure;
using AgriEnergyConnect.Webapp.Components;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF1cXmhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEFjXHxecXdWQGVZUUV2Vg==");
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSyncfusionBlazor();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
  
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

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();