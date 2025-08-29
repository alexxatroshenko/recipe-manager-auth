using Api.Common;
using Api.Extensions;
using Business.Infrastructure.Authentication.Commands;
using Business.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api.Endpoints;

public class Authentication: EndpointGroupBase
{
    public override void Map(RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost(SignUp);
    }

    private async Task<Ok<IdentityResponse>> SignUp(ISender sender, SignUpCommand request)
    {
        var result = await sender.Send(request);
        return TypedResults.Ok(result);
    }
}