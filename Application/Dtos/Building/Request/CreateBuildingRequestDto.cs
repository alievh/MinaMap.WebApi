using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Building.Request;

public class CreateBuildingRequestDto
{
    public IFormFile File { get; set; } = null!;
}
