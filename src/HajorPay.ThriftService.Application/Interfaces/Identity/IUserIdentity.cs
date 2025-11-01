namespace HajorPay.ThriftService.Application.Interfaces.Identity
{
    public interface IUserIdentity
    {
        Guid Id { get; }
        string Email { get; }
        string FirstName { get; }
        string LastName { get; }
    }
}
