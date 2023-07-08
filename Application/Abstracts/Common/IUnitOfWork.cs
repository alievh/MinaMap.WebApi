using Application.Abstracts.Repositories;

namespace Application.Abstracts.Common;

public interface IUnitOfWork
{
    IBuildingRepository BuildingRepository { get; }
    IPoiRepository PoiRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
