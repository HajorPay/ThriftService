using HajorPay.ThriftService.Application.Data.Interfaces;
using HajorPay.ThriftService.Domain.Entities;
using MediatR;

namespace HajorPay.ThriftService.Application.Features.GetGroupById
{
    public record GetGroupByIdQuery(Guid Id): IRequest<Group>;

    public class GetGroupByIdQueryHandler(IHajorPayDbContext _dbContext) : IRequestHandler<GetGroupByIdQuery, Group?>
    {
        public Task<Group?> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var group = _dbContext.Group.GetByIdAsync(request.Id);
            return group;
        }
    }
}
