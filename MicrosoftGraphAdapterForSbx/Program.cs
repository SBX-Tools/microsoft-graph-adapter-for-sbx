using System.Text.Json.Serialization;
using Microsoft.Identity.Web;
using MicrosoftGraphAdapterForSbx.Configuration;
using MicrosoftGraphAdapterForSbx.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureGraphComponent(builder.Configuration);
builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration, "SbxSettings");
builder.Services.AddControllers(options =>
    {
        options.Filters.Add<ServiceExceptionFilter>();
        options.Filters.Add<VersionHeaderFilter>();
        options.Filters.Add(new CompanyTenantIdFilter(builder.Configuration.GetSection("GraphSettings")["TenantId"] ?? throw new InvalidOperationException()));
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();