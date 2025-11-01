using HajorPay.ThriftService.Application.Data.Interfaces;
using HajorPay.ThriftService.Application.Interfaces;
using HajorPay.ThriftService.Application.Wrappers;
using HajorPay.ThriftService.Domain.Constants;
using HajorPay.ThriftService.Domain.Entities;
using HajorPay.ThriftService.Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;


namespace HajorPay.ThriftService.Application.Features.CreateGroup
{
    public class CreateGroupCommand : IRequest<Response<CreateGroupDto>> //TODO: Use record
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        //public required string ContributionAccount { get; set; }
        public required decimal ContributionAmount { get; set; }
        public ContributionFrequency ContributionFrequency { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public decimal LateFee { get; set; }
        public int NumberOfContributors { get; set; }
    }

    public class CreateGroupCommandHandler(IHajorPayDbContext _dbContext, IVirtualAccountService _virtualAccountService,Microsoft.Extensions.Logging.ILogger<CreateGroupCommand> logger) : IRequestHandler<CreateGroupCommand, Response<CreateGroupDto>>
    {
        public async Task<Response<CreateGroupDto>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var adminId = Guid.Parse("5A1F46B4-8719-4F82-1A92-08DD4BCCCEAC");
            if (await GroupExistsAsync(request.Name, adminId))
            {
                logger.LogInformation("Failed to create group: {GroupName} already exists under admin ({AdminId})", request.Name, adminId);
                //return Response<Guid>.Fail(ResponseMessages.GroupExists);
            }
            //TODO: Call external service to generate a virtual account for the group
            var virtualAccount = _virtualAccountService.GetANewVirtualAccount();
            if (string.IsNullOrEmpty(virtualAccount))
                throw new InvalidOperationException("Virtual account generation failed.");

            var group = new Group
            {
                GroupStatus = GroupStatus.NotStarted,
                AdminId = adminId, //TODO: Get this from claims
                Name = request.Name,
                Description = request.Description,
                ContributionAccount = virtualAccount,
                ContributionAmount = request.ContributionAmount,
                ContributionFrequency = request.ContributionFrequency,
                StartDate = request.StartDate,
                NumberOfContributors = request.NumberOfContributors
            };

            await _dbContext.Group.AddAsync(group);
            await _dbContext.SaveAsync(cancellationToken); //TODO: Should we pass cancellationToken?
            var groupId = group.Id;
            var createGroupResponseDto = new CreateGroupDto()
            {
                GroupId = groupId,
                GroupName = group.Name,
                VirtualAccount = virtualAccount,
                GroupStatus = group.GroupStatus
            };
            
            //ValueTask
            //            logger.LogInformation("New branch created: {BranchName} by user with Id ({UserId}) for organisation Id ({OrganisationId})", branch.Name, currentUserService.UserId, organisationId);

            //return Response<Guid>.Success(branch.Id, ResponseMessages.BranchCreated);

            return new Response<CreateGroupDto>(createGroupResponseDto);
        }

        private async Task<bool> GroupExistsAsync(string name, Guid adminId)
        {
            var query = _dbContext.Group.AsQueryable().Where(x => x.Name == name && x.AdminId == adminId);
            return await _dbContext.ExecuteAnyAsync(query);
        }
    }
}
