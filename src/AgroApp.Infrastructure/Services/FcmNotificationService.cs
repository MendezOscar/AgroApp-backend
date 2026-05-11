using AgroApp.Application.Common.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AgroApp.Infrastructure.Services;

public class FcmNotificationService : INotificationService
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<FcmNotificationService> _logger;

    public FcmNotificationService(
        IApplicationDbContext context,
        ILogger<FcmNotificationService> logger,
        IConfiguration configuration)
    {
        _context = context;
        _logger = logger;

        // Inicializar Firebase Admin solo una vez
        if (FirebaseApp.DefaultInstance is null)
        {
            var credentialJson = configuration["Firebase:CredentialJson"];
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromJson(credentialJson)
            });
        }
    }

    public async Task SendToUserAsync(Guid userId, string title, string body,
        Dictionary<string, string>? data = null)
    {
        var tokens = await _context.FcmTokens
            .Where(t => t.UserId == userId)
            .Select(t => t.Token)
            .ToListAsync();

        await _sendToTokensAsync(tokens, title, body, data);
    }

    public async Task SendToTenantAsync(Guid tenantId, string title, string body,
        Dictionary<string, string>? data = null)
    {
        var tokens = await _context.FcmTokens
            .Where(t => t.User.TenantId == tenantId)
            .Select(t => t.Token)
            .ToListAsync();

        await _sendToTokensAsync(tokens, title, body, data);
    }

    private async Task _sendToTokensAsync(List<string> tokens, string title,
        string body, Dictionary<string, string>? data)
    {
        if (!tokens.Any()) return;

        var message = new MulticastMessage
        {
            Tokens = tokens,
            Notification = new Notification { Title = title, Body = body },
            Data = data ?? new Dictionary<string, string>(),
            Android = new AndroidConfig
            {
                Priority = Priority.High,
                Notification = new AndroidNotification
                {
                    ChannelId = "agroapp_alerts",
                    Sound = "default"
                }
            },
            Apns = new ApnsConfig
            {
                Aps = new Aps { Sound = "default", Badge = 1 }
            }
        };

        try
        {
            var response = await FirebaseMessaging.DefaultInstance
                .SendEachForMulticastAsync(message);
            _logger.LogInformation(
                "FCM: {Success} enviados, {Failure} fallidos",
                response.SuccessCount, response.FailureCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error enviando notificación FCM");
        }
    }
}