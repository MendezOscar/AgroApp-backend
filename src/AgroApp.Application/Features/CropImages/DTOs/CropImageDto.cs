namespace AgroApp.Application.Features.CropImages.DTOs;

public record CropImageDto(
    Guid Id,
    Guid CropId,
    Guid UserId,
    string Url,
    string StorageKey,
    string? Category,
    string? AiDiagnosis,
    float? AiConfidence,
    string? DiagnosisCondition,
    DateTime? TakenAt,
    DateTime CreatedAt
);