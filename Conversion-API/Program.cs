using ConversionAPI.Cache;
using ConversionAPI.Repository;
using ConversionAPI.Services;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341"));

// Add services to the container.
builder.Services.AddScoped<ITemperatureConvertService, TemperatureConvertService>();
builder.Services.AddScoped<ILengthConvertService, LengthConvertService>();
builder.Services.AddScoped<IDataConvertService, DataConvertService>();
builder.Services.AddScoped<ITemperatureConvertRepository, TemperatureConvertRepository>();
builder.Services.AddScoped<ILengthConvertRepository, LengthConvertRepository>();
builder.Services.AddScoped<IDataConvertRepository, DataConvertRepository>();
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Conversion API.", Description = "Convert Metric units to Imperial units and vice versa." });
    c.EnableAnnotations();
});
builder.Services.AddSwaggerGenNewtonsoftSupport();
var app = builder.Build();

// Configure the HTTP request pipeline.

//Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Conversion API.");
    c.DocumentTitle = "Conversion API.";

});
app.UseReDoc(c =>
{
    c.RoutePrefix = "docs";  //http://localhost:5000/docs/
    c.SpecUrl("/swagger/v1/swagger.json");
    c.HideDownloadButton();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
