using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Carter;
using Microsoft.AspNetCore.Builder;

var host  = new WebHostBuilder()
.UseKestrel()
.ConfigureServices(services =>
{
    services.AddCarter();
})
.Configure(app =>
{
    app.UseExceptionHandler("/");
    app.UseRouting();
    app.UseEndpoints(builder => builder.MapCarter());
}).Build();
 
 host.Run();

