using NetTopologySuite.Features;

namespace Application.Dtos.Building.Response;

public class PoiResponseDto
{
    public string Type { get; set; }
    public IEnumerable<Feature> Features { get; set; }
}
