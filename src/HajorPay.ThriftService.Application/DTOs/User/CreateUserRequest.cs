namespace HajorPay.ThriftService.Application.DTOs.User
{
    public record CreateUserRequest(
    string FirstName,
    string LastName,
    string UserName,
    string EmailAddress,
    string NIN,
    string BVN, //TODO: Needed?
    bool OptInForSMS,
    string PhoneNumber,
    string Password);
}