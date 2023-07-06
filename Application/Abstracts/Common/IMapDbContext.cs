using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstracts.Common;

public interface IMapDbContext
{
    DbSet<Poi> poi { get; }
    DbSet<Build> builds { get; }
    DbSet<Domain.Entities.Path> paths { get; }
}
