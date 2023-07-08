using Application.Abstracts.Common;
using Domain.Entities;
using MediatR;

namespace Application.Handlers.Building.Commands;

public record DeleteBuildingCommand(int Id) : IRequest<bool>;

internal class DeleteBuildingCommandHandler : IRequestHandler<DeleteBuildingCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBuildingCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteBuildingCommand request, CancellationToken cancellationToken)
    {
        Build building = await _unitOfWork.BuildingRepository.GetAsync(n => n.OgcFid == request.Id)
            ?? throw new NullReferenceException();

        _unitOfWork.BuildingRepository.Remove(building);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
