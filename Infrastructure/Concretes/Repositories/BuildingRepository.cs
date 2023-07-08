using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Repositories;

public class BuildingRepository : Repository<Build>, IBuildingRepository
{
    public BuildingRepository(MapDbContext context) : base(context) { }
}
