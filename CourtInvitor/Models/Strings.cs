namespace CourtInvitor.Models
{
    internal class Strings
    {
        public const string Location = "Location";
        public const string CourtName = "Court Name";
        public const string Sunday = "Sunday";
        public const string Monday = "Monday";
        public const string Tuesday = "Tuesday";
        public const string Wednesday = "Wednesday";
        public const string Thursday = "Thursday";
        public const string Friday = "Friday";
        public const string Saturday = "Saturday";
        public const string AddNewCourt = "Add New Court";
        public const string CourtNamePlaceholder = "Court Name";
        public const string LocationPlaceholder = "Location";
        public const string SaveCourtButton = "Save Court";
        public const string BackButton = "Back";
        public const string EmptyString = "";



        public const string SaveProfileCommand = "SAVE";

        public const string FirstName = "enter your first name";

        public const string LastName = "enter your last name";
        public const string City = "Enter your City";
        public const string SignInWithGoogle = "SignIn with google";
        public const string MakeReservationText = "MakeReservation";
        public const string PlayerSearchText = "PlayerSearch";
        public const string MyReservationsText = "MyReservations";
        public const string ProfileText = "Profile";
        public const string RecievedSuggestionsText = "RecievedSuggestions";
        public const string UserName = "Enter your UserName:";
        public const string Email = "Enter your email:";
        public const string Password = "Enter your password:";
        public const string passwordRepeat = "Please repeat your password:";
        public const string LoginTitle = "Login to CourtInvitor!";
        public const string RegisterTitle = "Register to CourtInvitor!";
        public const string SubmitLoginButtonText = "Login!";
        public const string SubmitRegisterButtonText = "Register!";
        public const string DontOwnAccountText = "Don't have an account? ";
        public const string OwnAccountText = "Already have an account? ";
        public const string Register = "Register!";
        public const string Login = "Login!";
        public const string LoginOrRegisterButton = "Login/Register? Click me!";
        public const string SignOutButton = "Sign out.\nHope to see you soon!";
        public const string Welcome = "Welcome";
        public const string EmailExistsError = "The email address is already in use by another account.";
        public const string OperationNotAllowedError = "Unable to create an account in this method.";
        public const string WeakPasswordError = "The password is too weak. It should be at least 8 characters.";
        public const string MissingEmailError = "Please provide an email to create an account.";
        public const string MissingPasswordError = "Please provide a password to create an account.";
        public const string InvalidEmailError = "Please provide a valid email to create an account.";
        public const string InvalidCredentialsError = "One of the provided credentials (email/password) was incorrect.\n" +
            "Please re-check your input and try again.";
        public const string UserDisabledError = "Your account has been disabled.\n" +
            "Contact our team at 'shaysol1233@gmail.com' for more details.";
        public const string ManyAttemptsError = "There have been too many failed attempts, and your account has been temporarily disabled.\n" +
            "Please try again later.";
        public const string DefaultRegisterError = "Something went wrong, please try again later.\n" +
            "If the error persists, contact our developer team at 'shaysol1233@gmail.com'.";
        public const string FailedJsonError = "Something went wrong.\nThe system couldn't identify the error.\n" +
            "Please try again";
        public const string RegisterErrorTitle = "Error creating a user:";
        public const string LoginErrorTitle = "Error logging in:";
        public const string RegisterFailButton = "Understood!";
        public const string LoginFailButton = "I'll try again!";
        public const string RegisterSuccessTitle = "Account created successfully:";
        public const string LoginSuccessTitle = "User logged in successfully:";
        public const string RegisterSuccess = "Thank you for creating an account!\nEnjoy our CourtInvitor!";
        public const string LoginSuccess = "Welcome back!\nEnjoy our CourtInvitor!";
        public const string RegisterSuccessButton = "Hooray!";
        public const string LoginSuccessButton = "Hayde!";
        public const string LoginWithGoogleButtonText = "Login with Google!";
        public const string ForgotPassword = "Forgot your password?";
        public const string ClickMe = "Click me!";
        public const string ResetPWTitle = "An email has been sent:";
        public const string ResetPWErrorTitle = "Error sending a mail:";
        public const string ResetPWMessage = "An email with a link to reset your password has been sent to the provided email.\n" +
            "Please follow the instructions in the email and try again.";
        public const string ResetPWButton = "I will!";
        public const string ResetPWErrorButton = "I'll right it right away!";
        public const string EmailShortErrorTitle = "Email too short:";
        public string EmailShortErrorMessage = "The email you provided is too short.\n" +
            "Your email's minimum length must be " + ConstData.MinCharacterInEmail + " characters.\n" +
            "Please re-check it and try again.";
        public const string EmailShortErrorButton = "e-kay@gmail.com!";
        public const string EmailInvalidErrorTitle = "Invalid email:";
        public const string EmailInvalidErrorMessage = "The email you provided is invalid.\n" +
            "Please make sure it has '@' sign and a '.' and try again.";
        public const string EmailInvalidErrorButton = "ok@y.";
        public const string PasswordShortErrorTitle = "Password too short:";
        public string PasswordShortErrorMessage = "The password you provided is too short.\n" +
            "Your password's minimum length must be " + ConstData.MinCharacterInPW + " characters.\n" +
            "Please re-check it and try again.";
        public const string PasswordShortErrorButton = "oki-doki!";
        public const string PasswordNumberErrorTitle = "Password doesn't have a number:";
        public const string PasswordNumberErrorMessage = "Your password must contain at least one number.\n" +
            "Please add one (or any other number) and try again.";
        public const string PasswordNumberErrorButton = "5ure!";
        public const string PasswordLowerCaseErrorTitle = "Password doesn't have a lower-case letter:";
        public const string PasswordLowerCaseErrorMessage = "Your password must contain at least one lower-case letter.\n" +
            "Please add one and try again.";
        public const string PasswordLowerCaseErrorButton = "SURE!";
        public const string PasswordUpperCaseErrorTitle = "Password doesn't have an upper-case letter:";
        public const string PasswordUpperCaseErrorMessage = "Your password must contain at least one upper-case letter.\n" +
            "Please add one and try again.";
        public const string PasswordUpperCaseErrorButton = "sure!";
        public const string UserNameShortErrorTitle = "Username too short:";
        public string UserNameShortErrorMessage = "The username you provided is too short.\n" +
            "Your username's minimum length must be " + ConstData.MinCharacterInUN + " characters.\n" +
            "Please re-check it and try again.";
        public const string UserNameShortErrorButton = "FineThenOkay!";
        public const string UserNameNumberErrorTitle = "Username doesn't have a number:";
        public const string UserNameNumberErrorMessage = "Your username must contain at least one number.\n" +
            "Please add one (or any other number) and try again.";
        public const string UserNameNumberErrorButton = "0kay!";
        public const string SelectDay = "Days";
        public const string SelectHour = "Hours";
        public const string SaveDataDayHour = "Save Data";
        public const string Client = "Client";
        public const string Admin = "Admin";


        public const string AdminProfile = "Profile";
        public const string AdminCreateCourt = "Create Court";
        public const string AdminReservationsMade = "Reservations Made";
        public const string LocationIP = "https://nominatim.openstreetmap.org/search";



    }
}
