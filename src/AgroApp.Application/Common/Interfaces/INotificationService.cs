namespace AgroApp.Application.Common.Interfaces;

public interface INotificationService
{
    Task SendToUserAsync(Guid userId, string title, string body,
        Dictionary<string, string>? data = null);
    Task SendToTenantAsync(Guid tenantId, string title, string body,
        Dictionary<string, string>? data = null);
}