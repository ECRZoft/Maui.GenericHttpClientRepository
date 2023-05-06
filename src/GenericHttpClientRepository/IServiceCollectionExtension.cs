using GenericHttpClientRepository.Policies;
using Microsoft.Extensions.DependencyInjection;

namespace GenericHttpClientRepository;
public static class IServiceCollectionExtension
{
    public static IServiceCollection AddGenericHttpClientRepository(this IServiceCollection services)
    {
        services.AddSingleton<ClientPolicy>();
        services.AddSingleton<IGenericRepository, GenericRepository>();
        return services;
    }
}
