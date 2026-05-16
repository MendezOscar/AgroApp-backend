using AgroApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.CropImages.Commands;

public class AnalyzeImageCommandHandler
    : IRequestHandler<AnalyzeImageCommand, AnalyzeImageResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IAiService _aiService;
    private readonly ICurrentUserService _currentUser;

    public AnalyzeImageCommandHandler(
        IApplicationDbContext context,
        IAiService aiService,
        ICurrentUserService currentUser)
    {
        _context = context;
        _aiService = aiService;
        _currentUser = currentUser;
    }

    public async Task<AnalyzeImageResult> Handle(
        AnalyzeImageCommand request,
        CancellationToken cancellationToken)
    {
        // Obtener imagen y cultivo
        var image = await _context.CropImages
            .Include(i => i.Crop)
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
        image.AiConfidence = (decimal?)result.Confidence;

        await _context.SaveChangesAsync(cancellationToken);

        return new AnalyzeImageResult(
            result.Status,
            result.Condition,
            result.Confidence,
            result.Description,
            result.Recommendations);
    }
}