using HajorPay.ThriftService.Domain.Enums;

namespace HajorPay.ThriftService.Application.Features.CreateGroup
{
    public class CreateGroupDto
    {
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public string VirtualAccount { get; set; }
        public GroupStatus GroupStatus { get; set; }
    }
}
