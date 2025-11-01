namespace HajorPay.ThriftService.Domain.Constants
{
    public static class ResponseMessages
    {
        #region General Messages
        public const string NotFound = "The requested resource was not found.";
        public const string InternalServerError = "An unexpected error occurred. Please try again later.";
        public const string BadRequest = "The request is invalid. Please check your input and try again.";
        public const string OperationCanceled = "The operation was canceled by the user.";
        public const string Phisy = "An unexpected error occurred. Please check your request and try again later. If the problem persists, please contact support.";
        public const string Unauthorised = "You are not authorized to perform this action.";
        public const string OperationFailed = "The operation failed. Please try again.";
        public const string OperationNotAllowed = "This operation is not allowed.";
        public const string RecordExists = "The record already exists.";
        #endregion

        #region User Messages
        public const string UserCreated = "User created successfully. An email confirmation has been sent to the user.";
        public const string UserUpdated = "User updated successfully.";
        public const string UserDeleted = "User deleted successfully.";
        public const string UserAlreadyExists = "A user with this email already exists.";
        public const string UserCreationFailed = "Sorry, an error occurred while creating the user. Please try again.";
        public const string UserUpdateFailed = "Sorry, an error occurred while updating the user. Please try again.";
        public const string EmailAlreadyConfirmed = "The email address has already been confirmed.";
        #endregion

        #region Group Messages
        public const string GroupCreated = "Group created successfully.";
        public const string GroupUpdated = "Group updated successfully.";
        public const string GroupDeleted = "Group deleted successfully.";
        public const string GroupExists = "A group with this name already exists.";

        #endregion

    }
}
