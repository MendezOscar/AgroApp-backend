using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.CropImages.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.CropImages.Queries;

public class GetCropImagesQueryHandler : IRequestHandler<GetCropImagesQuery, List<CropImageDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetCropImagesQueryHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<List<CropImageDto>> Handle(GetCropImagesQuery request, CancellationToken cancellationToken)
    {
        return await _context.CropImages
            .Where(i => i.CropId == request.CropId
                     && i.Crop.Plot.Farm.TenantId == _currentUser.TenantId)
            .OrderByDescending(i => i.TakenAt ?? i.CreatedAt)
            .Select(i => new CropImageDto(
                i.Id, i.CropId, i.UserId,
                i.Url, i.StorageKey, i.Category,
                i.AiDiagnosis, i.AiConfidence,
                i.TakenAt, i.CreatedAt))
            .ToListAsync(cancellationToken);
    }
}