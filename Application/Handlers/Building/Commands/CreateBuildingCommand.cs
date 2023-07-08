using Application.Abstracts.Common;
using Application.Dtos.Building.Response;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

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
            Id = build.Id,
            Geometry = build.Geometry,
            AddrCity = build.AddrCity,
            AddrCountry = build.AddrCountry,
            AddrHousenumber = build.AddrHousenumber,
            AddrPostcode = build.AddrPostcode,
            AddrStreet = build.AddrStreet,
            Amenity = build.Amenity,
            BathOpenAir = build.BathOpenAir,
            BathSandBath = build.BathSandBath,
            Brand = build.Brand,
            Building = build.Building,
            BuildingLevels = build.BuildingLevels,
            Charge = build.Charge,
            Description = build.Description,
            Fee = build.Fee,
            Geotype = build.Geotype,
            Index = build.Index,
            InternetAccess = build.InternetAccess,
            InternetAccessFee = build.InternetAccessFee,
            Leisure = build.Leisure,
            Name = build.Name,
            NameAr = build.NameAr,
            NameAz = build.NameAz,
            NameEn = build.NameEn,
            NameRu = build.NameRu,
            OgcFid = build.OgcFid,
            OpeningHours = build.OpeningHours,
            OpeningHoursCovid19 = build.OpeningHoursCovid19,
            Operator = build.Operator,
            PaymentCash = build.PaymentCash,
            PaymentMastercard = build.PaymentMastercard,
            PaymentVisa = build.PaymentVisa,
            Phone = build.Phone,
            Phone1 = build.Phone1,
            Shop = build.Shop,
            Source = build.Source,
            Tourism = build.Tourism,
        };
    }
}
