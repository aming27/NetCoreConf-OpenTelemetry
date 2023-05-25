
using Microsoft.Extensions.Logging.ApplicationInsights;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAvengerDbContext();
builder.Services.AddApplicationInsightsTelemetry();
builder.Host.ConfigureLogging((context, builder) =>
{
    
    builder.AddApplicationInsights("6a67bc17-4911-4271-914a-129f16d2e8e6");
    builder.AddFilter<ApplicationInsightsLoggerProvider>(
        typeof(Program).FullName, LogLevel.Trace);

});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.MapType<HashedId>(() => new OpenApiSchema { Type = "string" }); c.CustomSchemaIds(x => x.FullName); });
builder.Services.AddHashedIds(op => op.Passphrase = "netcoreconf");
builder.Services.AddHypermedia();
builder.Services.AddAssembly();

var app = builder.Build();
app.SeedAvengerDbData(); // do not do that in your code. Data seed is only for demo purposes. If want to seed data, use a IHostedService.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.CatchOperationCanceled();
app.MapEndpoints()
   .WithHypermedia();

app.Run();