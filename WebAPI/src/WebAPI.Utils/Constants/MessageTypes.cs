namespace WebAPI.Utils.Constants
{
    public static class MessageTypes
    {
        public const string CreatedSuccessfull = "Created successfully.";
        public const string CreatedFailed = "Created failed.";

        public const string DeletedSuccessfull = "Deleted successfully.";
        public const string DeletedFailed = "Deleted failed.";

        public const string UpdatedSuccessfull = "Updated successfully.";
        public const string UpdatedFailed = "Updated failed.";

        public const string IdMustEmpty = "Id must be empty.";
        public const string NameMustFilled = "Name must be filled.";
        public const string NameMustShorter = "Name must be shorter.";

        public const string DepartmentNameMustFilled = "Department name must be filled.";
        public const string DepartmentNameMustShorter = "Department name must be shorter.";

        public const string DateMustFilled = "Date must be filled.";
        public const string DateMustShorter = "Date must be shorter.";

        public const string PhotoNameMustFilled = "Photo name must be filled.";
        public const string PhotoNameMustShorter = "Photo name must be shorter.";

        public const string ErrorMessage = @"An error occured seeding the database with test messages, Error:";
        public const string NullReference = "Object reference not set to an instance of an object.";

        public const string RegistrationSuccess = "User has been registered.";
        public const string RegistrationFailed = "User has been not registered.";
        
        public const string TokenGenerated = "Token generated.";
        public const string TokenInvalidGenerated = "Invalid email or password.";
        
        public const string UserNotExist = "The user doesn't exist.";
        public const string NamePhotoDefault = "anonymous.png";
    }
}