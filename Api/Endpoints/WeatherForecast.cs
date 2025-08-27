using Api.Common;
using Api.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Endpoints;

public class WeatherForecast: EndpointGroupBase
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet(GetWeatherForecasts);
    }
    
    public async Task<Ok> GetWeatherForecasts(ISender sender)
    {
        return TypedResults.Ok();
    }
}