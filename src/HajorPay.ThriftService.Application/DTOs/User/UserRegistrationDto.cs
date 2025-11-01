namespace HajorPay.ThriftService.Application.DTOs.User
{
    public record UserRegistrationDto(
    string FirstName,
    string LastName,
    string EmailAddress,
    string BVN,
    string NIN,
    bool OptInForSMS,
    bool OptInForEmail,
    bool PhoneNumber,
    string Password,
    string ConfirmPassword
);
}