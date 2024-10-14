namespace SlotEase.API.Constants.ApiResponse
{
    public static class MessageConstants
    {



        #region start user
        // Success Messages
        public const string SuccessMessage = "Congratulations! Your data has been saved successfully.";
        public const string UserCreatedSuccess = "User created successfully. Welcome aboard!";
        public const string UserUpdatedSuccess = "User updated successfully.";
        public const string UserDeletedSuccess = "User deleted successfully.";

        // Error Messages
        public const string EmailAlreadyInUse = "The email address you provided is already in use. Please check the email ID and try again.";
        public const string UserNotFound = "The specified user was not found. Please verify the user ID.";
        public const string InvalidData = "The provided data is invalid. Please check your input and try again.";
        public const string PasswordTooWeak = "The password you entered is too weak. Please use a stronger password.";
        public const string UnauthorizedAccess = "You do not have permission to perform this action.";
        public const string InternalServerError = "An unexpected error occurred. Please try again later.";


        //User Type Error
        public const string UserCreatiounUserTypeError = "UserType must be one of the following values:  (1)User,  (2)Admin,  (3)Vendor,  (4)Driver.";
        #endregion user



        // General Messages
        public const string OperationFailed = "The operation could not be completed. Please try again.";
        public const string DataRetrievalFailed = "An error occurred while retrieving data. Please try again.";

    }
}
