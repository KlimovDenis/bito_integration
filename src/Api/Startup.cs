using Carter;

namespace Api;

internal static class Startup
{
    public static void AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen();

        services.AddProblemDetails();
        
        services.AddCarter(new DependencyContextAssemblyCatalog());
    }

    public static void UseApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        
        app.MapCarter();
    }
}