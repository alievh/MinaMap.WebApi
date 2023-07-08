using Application.Abstracts.Common;
using Application.Dtos.Building.Response;
using Domain.Entities;
using MediatR;

namespace Application.Handlers.Building.Queries;

public record GetAllBuildingQuery : IRequest<IEnumerable<BuildingResponseDto>>;

internal class GetAllBuildingQueryHandler : IRequestHandler<GetAllBuildingQuery, IEnumerable<BuildingResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllBuildingQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<BuildingResponseDto>> Handle(GetAllBuildingQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Build> buildings = await _unitOfWork.BuildingRepository.GetAllAsync()
            ?? throw new NullReferenceException();

        return buildings.Select(build => new BuildingResponseDto
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
        });
    }
}
