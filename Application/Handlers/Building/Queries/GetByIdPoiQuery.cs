using Application.Abstracts.Common;
using Application.Dtos.Building.Response;
using Domain.Entities;
using MediatR;

namespace Application.Handlers.Building.Queries;

public record GetByIdPoiQuery(int Id) : IRequest<IEnumerable<PoiResponseDto>>;

internal class GetByIdPoiQueryHandler : IRequestHandler<GetByIdPoiQuery, IEnumerable<PoiResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetByIdPoiQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PoiResponseDto>> Handle(GetByIdPoiQuery request, CancellationToken cancellationToken)
    {
        Build building = await _unitOfWork.BuildingRepository.GetAsync(n => n.OgcFid == request.Id)
            ?? throw new NullReferenceException();

        // Getting POI which inside of building
        IEnumerable<Poi> pois = await _unitOfWork.PoiRepository.GetAllAsync(p => building.Geometry.Contains(p.Geometry));

        return pois.Select(n => new PoiResponseDto
        {
            OgcFid = n.OgcFid,
            Id = n.Id,
            AddrCity = n.AddrCity,
            AddrHousenumber = n.AddrHousenumber,
            AddrPostcode = n.AddrPostcode,
            AddrStreet = n.AddrStreet,
            AltName = n.AltName,
            Amenity = n.Amenity,
            Atm = n.Atm,
            Backrest = n.Backrest,
            Brand = n.Brand,
            BrandWikidata = n.BrandWikidata,
            BrandWikipedia = n.BrandWikipedia,
            ContactEmail = n.ContactEmail,
            ContactFacebook = n.ContactFacebook,
            ContactInstagram = n.ContactInstagram,
            ContactPhone = n.ContactPhone,
            ContactWebsite = n.ContactWebsite,
            Cuisine = n.Cuisine,
            Delivery = n.Delivery,
            DietHalal = n.DietHalal,
            DietMeat = n.DietMeat,
            DietVegan = n.DietVegan,
            DietVegetarian = n.DietVegetarian,
            DriveThrough = n.DriveThrough,
            Facebook = n.Facebook,
            Geometry = n.Geometry,
            Geotype = n.Geotype,
            Image = n.Image,
            Index = n.Index,
            InternetAccess = n.InternetAccess,
            InternetAccessFee = n.InternetAccessFee,
            Name = n.Name,
            NameAr = n.NameAr,
            NameAz = n.NameAz,
            NameEn = n.NameEn,
            NameFa = n.NameFa,
            NameRu = n.NameRu,
            NameTr = n.NameTr,
            OfficialName = n.OfficialName,
            OpeningHours = n.OpeningHours,
            OpeningHoursCovid19 = n.OpeningHoursCovid19,
            Operator = n.Operator,
            OutdoorSeating = n.OutdoorSeating,
            Phone = n.Phone,
            RefVatin = n.RefVatin,
            SourceRefUrl = n.SourceRefUrl,
            Takeaway = n.Takeaway,
            Website = n.Website,
            Wikidata = n.Wikidata,
            Wikipedia = n.Wikipedia
        });
    }
}
