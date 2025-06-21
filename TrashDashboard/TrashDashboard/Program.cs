using TrashDashboard.Components;
using TrashDashboard.ApiClient;
using TrashDashboard.Components.Layout;


namespace TrashDashboard;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services.AddBlazorBootstrap();

        builder.Services.AddHttpClient();

        builder.Services.AddHttpClient<Authorization>();
        builder.Services.AddSingleton<Authorization>();

        builder.Services.AddHttpClient<ApiClient.ApiClient>();
        builder.Services.AddScoped<ApiClient.ApiClient>();

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
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
