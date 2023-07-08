using Application.Abstracts.Repositories;
using Domain.Entities;
using Infrastructure.Concretes.Repositories.Base;
using Infrastructure.Persistance;

namespace Infrastructure.Concretes.Repositories;

public class PoiRepository : Repository<Poi>, IPoiRepository
{
    public PoiRepository(MapDbContext context) : base(context)
    {
    }
}
