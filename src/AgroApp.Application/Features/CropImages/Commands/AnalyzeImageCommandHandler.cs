using AgroApp.Application.Common.Interfaces;
using AgroApp.Domain.Entities;
using AgroApp.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.CropImages.Commands;

public class AnalyzeImageCommandHandler
    : IRequestHandler<AnalyzeImageCommand, AnalyzeImageResult>
{
    private const string PestAlertType = "pest_detected";

    private readonly IApplicationDbContext _context;
    private readonly IAiService _aiService;
    private readonly ICurrentUserService _currentUser;
    private readonly INotificationService _notifications;

    public AnalyzeImageCommandHandler(
        IApplicationDbContext context,
        IAiService aiService,
        ICurrentUserService currentUser,
        INotificationService notifications)
    {
        _context = context;
        _aiService = aiService;
        _currentUser = currentUser;
        _notifications = notifications;
    }

    public async Task<AnalyzeImageResult> Handle(
        AnalyzeImageCommand request,
        CancellationToken cancellationToken)
    {
        // Obtener imagen y cultivo
        var image = await _context.CropImages
            .Include(i => i.Crop).ThenInclude(c => c.Plot).ThenInclude(p => p.Farm)
            .FirstOrDefaultAsync(i =>
                i.Id == request.ImageId &&
                i.CropId == request.CropId,
                cancellationToken)
            ?? throw new Exception("Imagen no encontrada");

        // Llamar al servicio de IA
        var result = await _aiService.AnalyzeImageAsync(
            image.Url,
            image.Crop.CropType,
            request.CropId.ToString(),
            request.ImageId.ToString());

        // Guardar diagnóstico
        var diagnosisJson =
            System.Text.Json.JsonSerializer.Serialize(
                new
                {
                    status = result.Status,
                    condition = result.Condition,
                    confidence = result.Confidence,
                    description = result.Description,
                    recommendations = result.Recommendations,
                    analyzedAt = DateTime.UtcNow
                });

        image.AiDiagnosis = diagnosisJson;
        image.AiAnalyzedAt = DateTime.UtcNow;
        image.AiConfidence = result.Confidence; // ← redondear
        image.DiagnosisCondition = result.Condition;

        if (!string.Equals(result.Status, "healthy", StringComparison.OrdinalIgnoreCase)
            && result.Confidence >= request.ConfidenceThreshold)
        {
            var tenantId = image.Crop.Plot.Farm.TenantId;
            var message = $"Posible {result.Condition} detectado en {image.Crop.CropType} " +
                          $"(confianza {result.Confidence:P0}).";

            _context.Alerts.Add(new Alert
            {
                TenantId = tenantId,
                PlotId = image.Crop.PlotId,
                CropId = image.CropId,
                AlertType = PestAlertType,
                Severity = result.Confidence >= 0.8f
                    ? AlertSeverity.Critical
                    : AlertSeverity.Warning,
                Message = message,
                TriggeredAt = DateTime.UtcNow,
            });

            await _notifications.SendToTenantAsync(tenantId,
                title: "🐛 Posible plaga o enfermedad",
                body: message,
                data: new Dictionary<string, string>
                {
                    ["cropId"] = image.CropId.ToString(),
                    ["type"] = PestAlertType,
                });
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new AnalyzeImageResult(
            result.Status,
            result.Condition,
            result.Confidence,
            result.Description,
            result.Recommendations);
    }
}