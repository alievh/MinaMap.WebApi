using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstracts.Common;

public interface IMapDbContext
{
    DbSet<Poi> POI { get; }
    DbSet<Build> Builds { get; }
    DbSet<Domain.Entities.Path> Paths { get; }
}
