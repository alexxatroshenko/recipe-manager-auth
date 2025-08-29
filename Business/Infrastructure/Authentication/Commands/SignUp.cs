using Business.Common.Exceptions;
using Business.Infrastructure.Services.Interfaces;
using Business.Models;
using Data.Db.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Business.Infrastructure.Authentication.Commands;

public record SignUpCommand : IRequest<IdentityResponse>
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string Username { get; init; } = null!;
}

public class SignUp: IRequestHandler<SignUpCommand, IdentityResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public SignUp(UserManager<User> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }
    
    public async Task<IdentityResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        
        if (existingUser != null)
            throw new UserExistsException("Пользователь с таким email уже существует");
        
        existingUser = await _userManager.FindByNameAsync(request.Username);
        if (existingUser != null)
            throw new UserExistsException("Пользователь с таким именем уже существует");

        var user = new User
        {
            UserName = request.Username,
            Email = request.Email,
            EmailConfirmed = false,
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        return _tokenService.GenerateTokens(user, new List<string> { "default" });
    }
}