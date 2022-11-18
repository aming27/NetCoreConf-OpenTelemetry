using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var url = Environment.GetEnvironmentVariable("OTEL_COLLECTOR_URL");
builder.Services.AddOpenTelemetryMetrics(builder =>
{
    builder
        .SetResourceBuilder(ResourceBuilder
            .CreateDefault()
            .AddService("FrontEnd"));    
    builder.AddOtlpExporter(options => options.Endpoint = new Uri(url));
    builder.AddConsoleExporter();
});

// Configure tracing
builder.Services.AddOpenTelemetryTracing(builder =>
{

    builder
        .SetResourceBuilder(ResourceBuilder
            .CreateDefault()
            .AddService("FrontEnd"));
    builder.AddAspNetCoreInstrumentation();    
    builder.AddOtlpExporter(options => options.Endpoint = new Uri(url));
    builder.AddConsoleExporter();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
