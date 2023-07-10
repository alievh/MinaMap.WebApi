using Application.Abstracts.Common;
using Application.Dtos.Building.Response;
using Domain.Entities;
using MediatR;
using NetTopologySuite.Features;

namespace Application.Handlers.Building.Queries;

public record GetByIdBuildingQuery(int Id) : IRequest<BuildingResponseDto>;

internal class GetByIdBuildingQueryHandler : IRequestHandler<GetByIdBuildingQuery, BuildingResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdBuildingQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BuildingResponseDto> Handle(GetByIdBuildingQuery request, CancellationToken cancellationToken)
    {
        Build build = await _unitOfWork.BuildingRepository.GetAsync(n => n.OgcFid == request.Id)
            ?? throw new NullReferenceException();

        return new BuildingResponseDto
        {
            Type = "FeatureCollection",
            Features = new List<Feature>
            {
                new Feature
                {
                    Geometry = build.Geometry
                }
            }
        };
    }
}
