namespace AgroApp.Application.Features.Phenology.DTOs;

public record PhenologyTemplateDto(
    Guid Id,
    string CropType,
    string StageName,
    int StageOrder,
    string? Description,
    int MinDays,
    int MaxDays,
    string? Icon,
    string? Recommendations
);

public record PhenologyStageDto(
    Guid Id,
    Guid CropId,
    Guid? TemplateId,
    string StageName,
    int StageOrder,
    string? Icon,
    DateOnly StartedAt,
    DateOnly? EndedAt,
    string? Observations,
    bool IsCustom,
    bool IsActive,
    int DaysInStage,
    DateTime CreatedAt
);

public record CreatePhenologyStageRequest(
    Guid? TemplateId,
    string StageName,
    int StageOrder,
    DateOnly StartedAt,
    string? Observations,
    bool IsCustom
);

public record UpdatePhenologyStageRequest(
    DateOnly? EndedAt,
    string? Observations
);