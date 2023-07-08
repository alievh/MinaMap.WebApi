using Application.Abstracts.Common;
using Application.Abstracts.Repositories;
using Infrastructure.Concretes.Repositories;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Common;

public class UnitOfWork : IUnitOfWork
{
    readonly MapDbContext _dbContext;

    public UnitOfWork(MapDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private IBuildingRepository? _buildingRepository;
    private IPoiRepository? _poiRepository;

    public IBuildingRepository BuildingRepository => _buildingRepository ??= new BuildingRepository(_dbContext);
    public IPoiRepository PoiRepository => _poiRepository ??= new PoiRepository(_dbContext);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await _dbContext.SaveChangesAsync(cancellationToken);
}
