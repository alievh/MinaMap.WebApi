using Application.Abstracts.Common;
using Application.Dtos.Building.Response;
using Application.Extensions;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Handlers.Building.Commands;

public record UpdateBuildingCommand(int Id, IFormFile File) : IRequest<BuildingResponseDto>;

internal class UpdateBuildingCommandHandler : IRequestHandler<UpdateBuildingCommand, BuildingResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBuildingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BuildingResponseDto> Handle(UpdateBuildingCommand request, CancellationToken cancellationToken)
    {
        Build build = await _unitOfWork.BuildingRepository.GetAsync(n => n.OgcFid == request.Id)
            ?? throw new NullReferenceException();

        Build convertedBuild = request.File.ConvertGeoJsonToObject<Build>();

        build.Id = convertedBuild.Id;
        build.Geometry = convertedBuild.Geometry;
        build.AddrCity = convertedBuild.AddrCity;
        build.AddrCountry = convertedBuild.AddrCountry;
        build.AddrHousenumber = convertedBuild.AddrHousenumber;
        build.AddrPostcode = convertedBuild.AddrPostcode;
        build.AddrStreet = convertedBuild.AddrStreet;
        build.Amenity = convertedBuild.Amenity;
        build.BathOpenAir = convertedBuild.BathOpenAir;
        build.BathSandBath = convertedBuild.BathSandBath;
        build.Brand = convertedBuild.Brand;
        build.Building = convertedBuild.Building;
        build.BuildingLevels = convertedBuild.BuildingLevels;
        build.Charge = convertedBuild.Charge;
        build.Description = convertedBuild.Description;
        build.Fee = convertedBuild.Fee;
        build.Geotype = convertedBuild.Geotype;
        build.Index = convertedBuild.Index;
        build.InternetAccess = convertedBuild.InternetAccess;
        build.InternetAccessFee = convertedBuild.InternetAccessFee;
        build.Leisure = convertedBuild.Leisure;
        build.Name = convertedBuild.Name;
        build.NameAr = convertedBuild.NameAr;
        build.NameAz = convertedBuild.NameAz;
        build.NameEn = convertedBuild.NameEn;
        build.NameRu = convertedBuild.NameRu;
        build.OpeningHours = convertedBuild.OpeningHours;
        build.OpeningHoursCovid19 = convertedBuild.OpeningHoursCovid19;
        build.Operator = convertedBuild.Operator;
        build.PaymentCash = convertedBuild.PaymentCash;
        build.PaymentMastercard = convertedBuild.PaymentMastercard;
        build.PaymentVisa = convertedBuild.PaymentVisa;
        build.Phone = convertedBuild.Phone;
        build.Phone1 = convertedBuild.Phone1;
        build.Shop = convertedBuild.Shop;
        build.Source = convertedBuild.Source;
        build.Tourism = convertedBuild.Tourism;

        _unitOfWork.BuildingRepository.Update(build);
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
