using System.ComponentModel.DataAnnotations;
using MongoReadModel.Configuration;

namespace MongoReadModel.Extensions; 

public static class OptionsExtension
{
    public static ClientOptions MessageClientOptions(this IServiceCollection services, IConfiguration configuration)
    {
        var appOptions = new ClientOptions();
        configuration.GetSection("MessageClientOptions").Bind(appOptions);

        services.Configure<ClientOptions>(configuration.GetSection("MessageClientOptions"));

        ICollection<ValidationResult> results = new List<ValidationResult>();
        var validated = Validator.TryValidateObject(appOptions, new ValidationContext(appOptions), results, true);
        if (!validated)
            throw new Exception(
                $"You're probably missing an environment variable / appsettings.json / repo secret on github. Here's the technical error: " +
                $"{string.Join(", ", results.Select(r => r.ErrorMessage))}");

        return appOptions;
    }
}