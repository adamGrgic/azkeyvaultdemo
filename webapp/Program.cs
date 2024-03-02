using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

SecretClientOptions options = new SecretClientOptions()
{
    Retry =
        {
            Delay= TimeSpan.FromSeconds(2),
            MaxDelay = TimeSpan.FromSeconds(16),
            MaxRetries = 5,
            Mode = RetryMode.Exponential
         }
};
var client = new SecretClient(new Uri("https://kvtestdemo69696969.vault.azure.net/"), new DefaultAzureCredential(), options);

var secret = client.GetSecret("ExamplePassword");

var secretValue = secret.Value;

app.MapGet("/", () => secretValue);

app.Run();
