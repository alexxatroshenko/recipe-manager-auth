using Api.Common;
using Api.Extensions;
using Business.Infrastructure.WeatherForecasts.Queries;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Endpoints;

public class WeatherForecasts: EndpointGroupBase
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapGet(GetWeatherForecasts);
    }

    private async Task<Ok<IEnumerable<WeatherForecast>>> GetWeatherForecasts(ISender sender)
    {
        var result = await sender.Send(new GetWeatherForecastsQuery());
        return TypedResults.Ok(result);
    }
}