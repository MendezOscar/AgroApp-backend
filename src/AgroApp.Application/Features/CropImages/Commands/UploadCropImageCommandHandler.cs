using AgroApp.Application.Common.Interfaces;
using AgroApp.Application.Features.CropImages.DTOs;
using AgroApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.CropImages.Commands;

public class UploadCropImageCommandHandler : IRequestHandler<UploadCropImageCommand, CropImageDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IStorageService _storage;
    private readonly ICurrentUserService _currentUser;

    public UploadCropImageCommandHandler(
        IApplicationDbContext context,
        IStorageService storage,
        ICurrentUserService currentUser)
    {
        _context = context;
        _storage = storage;
        _currentUser = currentUser;
    }

    public async Task<CropImageDto> Handle(UploadCropImageCommand request, CancellationToken cancellationToken)
    {
        var crop = await _context.Crops
            .Include(c => c.Plot.Farm)
            .FirstOrDefaultAsync(c => c.Id == request.CropId
                                   && c.Plot.Farm.TenantId == _currentUser.TenantId,
                                   cancellationToken);

        if (crop is null)
            throw new InvalidOperationException("Cultivo no encontrado.");

        // Subir a R2
        var storageKey = await _storage.UploadAsync(
            request.FileStream,
            request.FileName,
            request.ContentType,
            cancellationToken);

        // Construir URL pública
        var url = $"{storageKey}";

        var image = new CropImage
        {
            CropId = request.CropId,
            UserId = _currentUser.UserId,
            Url = url,
            StorageKey = storageKey,
            Category = request.Category,
            TakenAt = request.TakenAt
        };

        _context.CropImages.Add(image);
        await _context.SaveChangesAsync(cancellationToken);

        return new CropImageDto(
            image.Id, image.CropId, image.UserId,
            image.Url, image.StorageKey, image.Category,
            image.AiDiagnosis, image.AiConfidence, image.DiagnosisCondition,
            image.TakenAt, image.CreatedAt);
    }
}