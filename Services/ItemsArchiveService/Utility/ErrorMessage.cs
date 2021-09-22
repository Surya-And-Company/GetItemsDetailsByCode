namespace ItemsArchiveService.Utility
{
    public class ErrorMessage
    {
        private ErrorMessage() { }

        public const string RequiredMobileNo = "Please provide mobile no!";
        public const string InvalidMobileNo = "Invalid mobile no!";
        public const string RequiredName = "Please enter name!";
        public const string InvalidName = "Name should be 3 to 25 character!";

        public const string RequiredRole = "Please select role!";
        public const string InvalidRole = "Invalid role!";

    }
}