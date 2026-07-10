using AgroApp.Application.Features.CropImages.DTOs;
using MediatR;

namespace AgroApp.Application.Features.CropImages.Queries;

public record GetPestDiagnosisSummaryQuery(Guid FarmId) : IRequest<List<PestDiagnosisSummaryDto>>;
