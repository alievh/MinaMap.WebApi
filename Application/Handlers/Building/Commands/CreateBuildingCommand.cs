using Application.Abstracts.Common;
using Application.Dtos.Building.Response;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using NetTopologySuite.Features;

namespace Application.Handlers.Building.Commands;

public record CreateBuildingCommand(IFormFile File) : IRequest<BuildingResponseDto>;

internal class CreateBuildingCommandHandler : IRequestHandler<CreateBuildingCommand, BuildingResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateBuildingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BuildingResponseDto> Handle(CreateBuildingCommand request, CancellationToken cancellationToken)
    {
        Build building = request.File.ConvertGeoJsonToObject<Build>();
        Build build = await _unitOfWork.BuildingRepository.AddAsync(building);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

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
