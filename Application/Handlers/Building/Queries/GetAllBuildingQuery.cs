using Application.Abstracts.Common;
using Application.Dtos.Building.Response;
using Domain.Entities;
using MediatR;
using NetTopologySuite.Features;

namespace Application.Handlers.Building.Queries;

public record GetAllBuildingQuery : IRequest<BuildingResponseDto>;

internal class GetAllBuildingQueryHandler : IRequestHandler<GetAllBuildingQuery, BuildingResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllBuildingQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BuildingResponseDto> Handle(GetAllBuildingQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Build> buildings = await _unitOfWork.BuildingRepository.GetAllAsync()
            ?? throw new NullReferenceException();

        BuildingResponseDto responseDto = new BuildingResponseDto();
        responseDto.Type = "FeatureCollection";
        responseDto.Features = buildings.Select(build => new Feature
        {
            Geometry = build.Geometry
        });

        return new BuildingResponseDto
        {
            Type = "FeatureCollection",
            Features = buildings.Select(build => new Feature
            {
                Geometry = build.Geometry
            })
        };
    }
}
