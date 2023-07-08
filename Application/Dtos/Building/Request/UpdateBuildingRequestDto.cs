using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Building.Request;

public class UpdateBuildingRequestDto
{
    public IFormFile File { get; set; } = null!;
}
