using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FrontEnd;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configuraciones basadas en el entorno
var configuration = builder.Configuration;
var env = builder.HostEnvironment;

builder.Services.AddConfigureServices(configuration, env);

await builder.Build().RunAsync();
