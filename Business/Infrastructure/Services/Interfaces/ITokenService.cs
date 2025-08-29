using Business.Models;
using Data.Db.Entities;

namespace Business.Infrastructure.Services.Interfaces;

public interface ITokenService
{
    IdentityResponse GenerateTokens(User user, List<string> roles);
}