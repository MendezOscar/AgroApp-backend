using AgroApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgroApp.Application.Features.CropImages.Commands;

public class DeleteCropImageCommandHandler : IRequestHandler<DeleteCropImageCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IStorageService _storage;
    private readonly ICurrentUserService _currentUser;

    public DeleteCropImageCommandHandler(
        IApplicationDbContext context,
        IStorageService storage,
        ICurrentUserService currentUser)
    {
        _context = context;
        _storage = storage;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(DeleteCropImageCommand request, CancellationToken cancellationToken)
    {
        var image = await _context.CropImages
            .Include(i => i.Crop.Plot.Farm)
            .FirstOrDefaultAsync(i => i.Id == request.Id
                                   && i.CropId == request.CropId
                                   && i.Crop.Plot.Farm.TenantId == _currentUser.TenantId,
                                   cancellationToken);

        if (image is null) return false;

        await _storage.DeleteAsync(image.StorageKey, cancellationToken);
        _context.CropImages.Remove(image);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}