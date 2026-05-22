using AgroApp.Domain.Entities;

namespace AgroApp.Application.Common.Interfaces;

public interface IAuthService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
    string GenerateRefreshToken();
    RefreshToken CreateRefreshToken(Guid userId);
    string GenerateJwtToken(Guid userId, string email, string role, Guid tenantId);

}