using System.Reflection;
using Api.Common;

namespace Api.Extensions;

public static class MapEndpointsExtensions
{
    private static RouteGroupBuilder MapGroup(this WebApplication app, EndpointGroupBase group)
    {
        var groupName = group.GroupName ?? group.GetType().Name;
        return app
            .MapGroup($"/api/{groupName}")
            .WithGroupName(groupName)
            .WithTags(groupName);
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var endpointGroupType = typeof(EndpointGroupBase);

        var assembly = Assembly.GetExecutingAssembly();

        var endpointGroupTypes = assembly.GetExportedTypes()
            .Where(t => t.IsSubclassOf(endpointGroupType));

        foreach (var type in endpointGroupTypes)
        {
            if (Activator.CreateInstance(type) is EndpointGroupBase instance)
            {
                instance.Map(app.MapGroup(instance));
            }
        }
        return app;
    }
    
    public static void MapGet(this IEndpointRouteBuilder builder, Delegate handler)
    {
        builder.MapGet(handler.Method.Name, handler);
    }

    public static RouteHandlerBuilder MapPost(this IEndpointRouteBuilder builder, Delegate handler)
    {
        return builder.MapPost(handler.Method.Name, handler);
    }

    public static RouteHandlerBuilder MapPut(this IEndpointRouteBuilder builder, Delegate handler)
    {
        return builder.MapPut(handler.Method.Name, handler);
    }

    public static RouteHandlerBuilder MapDelete(this IEndpointRouteBuilder builder, Delegate handler)
    {
        return builder.MapDelete(handler.Method.Name, handler);
    }
}