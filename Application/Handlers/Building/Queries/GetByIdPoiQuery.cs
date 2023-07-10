using Application.Abstracts.Common;
using Application.Dtos.Building.Response;
using Domain.Entities;
using MediatR;
using NetTopologySuite.Features;

namespace Application.Handlers.Building.Queries;

public record GetByIdPoiQuery(int Id) : IRequest<PoiResponseDto>;

internal class GetByIdPoiQueryHandler : IRequestHandler<GetByIdPoiQuery, PoiResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdPoiQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PoiResponseDto> Handle(GetByIdPoiQuery request, CancellationToken cancellationToken)
    {
        Build building = await _unitOfWork.BuildingRepository.GetAsync(n => n.OgcFid == request.Id)
            ?? throw new NullReferenceException();

        // Getting POI which inside of building
        IEnumerable<Poi> pois = await _unitOfWork.PoiRepository.GetAllAsync(p => building.Geometry.Contains(p.Geometry));

        return new PoiResponseDto
        {
            Type = "FeatureCollection",
            Features = pois.Select(poi => new Feature
            {
                Geometry = poi.Geometry
            })
        };
    }
}
