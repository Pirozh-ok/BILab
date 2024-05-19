namespace BILab.Domain {
    public class ResponseConstants {
        public const string InvalidAccessOrRefreshToken = "Invalid access or refresh token";
        public const string InternalServerError = "Internal server error";
        public const string AccessDenied = "Access denied";
        public const string ChechkEmail = "Please check your email for the verification action";
        public const string ConfirmUpdatedEmail = "Hi {0}!&lt;br&gt;This email was sent to you because you changed the email address of your kinopoisk account.&lt;br&gt;Please confirm your new email by following this link: {1}";
        public const string CouldntLogIn = "Couldn't log in, try again later";
        public const string EmailAlreadyConfirmed = "The user's email has already been confirmed";
        public const string EmailConfirmed = "Thank you for confirming your email";
        public const string EmailExceedsMaxLen = "Invalid email value. The length of the email should not exceed 50 characters"; 
        public const string EmailLessMinLen = "Invalid email value. The minimum length of the email is 2";
        public const string EmailNotFound = "The user's email was not found";
        public const string EmailPasswordChanged = "Your account password has been changed";
        public const string FailedRegister = "Failed to register, try again later";
        public const string FirstNameExceedsMaxLen = "Invalid first name value. The length of the user first name should not exceed 50 characters";
        public const string FirstNameLessMinLen = "Invalid first name value. The minimum length of the first name  is 2";
        public const string NameExceedsMaxLen = "Invalid name value. The length of the name should not exceed 50 characters";
        public const string NameLessMinLen = "Invalid name value. The minimum length of the name  is 2";
        public const string IdIsRequired = "Id is required";
        public const string IncorrectedSex = "Sex is incorrected";
        public const string IncorrectedDay = "Date of day is ncorrected";
        public const string IncorrectPhoneNumber = "Phone number is incorrected";
        public const string IncorrectDateOfBirth = "Incorrect date of birth value";
        public const string InvalidEmailOrPassword = "Invalid email address or password";
        public const string LastNameExceedsMaxLen = "Invalid last name value. The length of the user last name should not exceed 50 characters";
        public const string LastNameLessMinLen = "Invalid last name value. The minimum length of the lastname  is 2";
        public const string UserNotFound = "User not found";
        public const string PasswordChanged = "The password has been successfully changed";
        public const string PasswordExceedsMaxLen = "Invalid password value. The length of the password should not exceed 30 characters";
        public const string PasswordLessMinLen = "Invalid password value. The minimum length of the password is 6";
        public const string PasswordsNotMatch = "The entered passwords do not match";
        public const string PatronymicExceedsMaxLen = "Invalid patronymic value. The length of the user patronymic should not exceed 50 characters";
        public const string PatronymicLessMinLen = "Invalid patronymic value. The minimum length of the patronymic is 2";
        public const string SubjectConfirmEmail = "Confirm your account";
        public const string SubjectPasswordChanged = "Password changed";
        public const string SubjectResetPassword = "Reset password";
        public const string SubjectCloseRecord = "Отмена записи на процедуру";
        public const string TextConfirmEmail = "Hi {0}! &lt;br&gt; You have been sent this email because you created an account on BILab .&lt;br&gt; Please confirm your account by clicking this link: {1}";
        public const string TextResetEmail = "Hi {0}! &lt;br&gt; You have received this email because you have started password recovery. If it wasn't you, ignore this email. &lt;br&gt; To reset password clicking this link: {1}";
        public const string RecordWasClosedEmail = "Здравствуйте, {0}! &lt;br&gt; Ваша запись на процедуру {1} время {2} отменена";

        public const string RecordNotFound = "Record not found";
        public const string RecordWasClosed = "Record was closed";
        //public const string UserNotFound = "";
        //public const string UserNotFound = "";
        //public const string UserNotFound = "";
        //public const string UserNotFound = "";
        //public const string UserNotFound = "";
        //public const string UserNotFound = "";
        //public const string UserNotFound = "";
        //public const string UserNotFound = "";
        //public const string UserNotFound = "";

        public const string NotFound = "Not found";
        public const string NullArgument = "Null argument";
        public const string Created = "Entity created";
        public const string Deleted = "Entity deleted";
        public const string Updated = "Entity updated";

        //Roles messages
        public const string RoleNameExceedMaxLen = "Role name exceeds the maximum allowed length {0}";
        public const string RoleNameLessMinLen = "Role name is less than the minimum allowed length {0}";
        public const string RoleNotFound = "Role not found";
        public const string RoleNullOrEmptyName = "Role name must not be null or empty";
        public const string RoleAddedToUser = "Roles have been successfully added to the user";
        public const string RoleChanged = "Role name changed";
        public const string RoleCreated = "Role created successfully";
        public const string RoleDeleted = "RoleDeleted";
        public const string RoleNotExist = "Role named {0} does not exist.";
        public const string RoleRemovedFromUser = "Roles have been successfully removed from the user";
        public const string RoleUpdated = "Role updated successfully";
    }
}
