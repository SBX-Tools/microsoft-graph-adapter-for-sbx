using System.Security.Cryptography.X509Certificates;
using Azure.Identity;
using Microsoft.Graph;
using MicrosoftGraphAdapterForSbx.Settings;
using File = System.IO.File;

namespace MicrosoftGraphAdapterForSbx.Configuration;

public static class GraphConfiguration
{
    public static IServiceCollection ConfigureGraphComponent(this IServiceCollection services, IConfiguration configuration)
    {
        var graphSettings = configuration.GetSection(nameof(GraphSettings)).Get<GraphSettings>() ?? throw new InvalidOperationException();

        var bytes = File.ReadAllBytes(graphSettings.CertificatePath);
        var certificate = new X509Certificate2(bytes, graphSettings.CertificatePassword);

        var clientCertificateCredential = new ClientCertificateCredential(graphSettings.TenantId, graphSettings.ClientId, certificate);

        services.AddSingleton(_ => new GraphServiceClient(clientCertificateCredential));

        return services;
    }
}