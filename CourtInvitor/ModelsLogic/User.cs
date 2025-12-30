using CourtInvitor.Models;
using Plugin.CloudFirestore;
namespace CourtInvitor.ModelsLogic
{
    /// <summary>
    /// Implementation of user authentication and management.
    /// </summary>
    public class User : UserModel
    {
        #region Fields
        private readonly FbData fbData;
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the User class.
        /// </summary>
        public User()
        {
            fbData = new FbData();
        }
        #endregion
        #region Public Functions
        /// <summary>
        /// Checks if the user can register with current credentials.
        /// </summary>
        /// <returns>True if registration is allowed.</returns>
        public override bool CanRegister()
        {
            return IsEmailValid() && IsPasswordValid() && IsUserNameValid() && IsRoleValid();
        }
        /// <summary>
        /// Registers the user with Firebase.
        /// </summary>
        /// <returns>True if registration succeeded.</returns>
        public override async Task<bool> Register()
        {
            return await fbData.CreateUserWithEmailAndPWAsync(
                Email,
                Password,
                UserName,
                OnRegisterCompleted);
        }
        /// <summary>
        /// Signs out the current user.
        /// </summary>
        public override void SignOut()
        {
            fbData.SignOut();
            Preferences.Clear();
        }
        /// <summary>
        /// Logs in the user with current credentials.
        /// </summary>
        /// <returns>True if login succeeded.</returns>
        public override async Task<bool> Login()
        {
            bool loginSucceeded = false;
            if (!IsEmailValidLogin())
            {
                await Shell.Current.DisplayAlert(
                    Strings.LoginErrorTitle,
                    Strings.EmailInvalid,
                    Strings.Ok);
            }
            else if (!IsPasswordValidLogin())
            {
                await Shell.Current.DisplayAlert(
                    Strings.LoginErrorTitle,
                    Strings.PasswordEmpty,
                    Strings.Ok);
            }
            else
            {
                try
                {
                    loginSucceeded = await fbData.SignInWithEmailAndPWdAsync(
                        Email,
                        Password,
                        OnCompleteLogin);
                    if (!loginSucceeded)
                        await Shell.Current.DisplayAlert(
                            Strings.LoginErrorTitle,
                            Strings.InvalidCredentialsError,
                            Strings.Ok);
                }
                catch
                {
                    await Shell.Current.DisplayAlert(
                        Strings.LoginErrorTitle,
                        Strings.InvalidCredentialsError,
                        Strings.Ok);
                }
            }
            return loginSucceeded;
        }
        #endregion
        #region Private Functions
        /// <summary>
        /// Identifies Firebase error from task exception.
        /// </summary>
        /// <param name="task">The failed task.</param>
        /// <returns>User-friendly error message.</returns>
        private static string IdentifyFirebaseError(Task task)
        {
            string errorMessage = Strings.RegisterFailed;
            Exception? exception = task.Exception?.InnerException;
            if (exception != null)
            {
                string message = exception.Message;
                if (message.Contains(Keys.EmailExistsErrorKey))
                    errorMessage = Strings.EmailExistsError;
                else if (message.Contains(Keys.WeakPasswordErrorKey))
                    errorMessage = Strings.WeakPasswordError;
                else if (message.Contains(Keys.InvalidEmailErrorKey))
                    errorMessage = Strings.EmailInvalid;
                else if (message.Contains(Keys.ManyAttemptsErrorKey))
                    errorMessage = Strings.ManyAttemptsError;
            }
            return errorMessage;
        }
        /// <summary>
        /// Handles registration completion.
        /// </summary>
        /// <param name="task">The registration task.</param>
        /// <returns>True if registration completed successfully.</returns>
        private async Task<bool> OnRegisterCompleted(Task task)
        {
            bool success = false;
            if (!task.IsCompletedSuccessfully)
            {
                string message = IdentifyFirebaseError(task);
                await Shell.Current.DisplayAlert(
                    Strings.RegisterErrorTitle,
                    message,
                    Strings.Ok);
            }
            else
            {
                string userId = fbData.UserId;
                if (!string.IsNullOrEmpty(userId))
                {
                    fbData.SetDocument(
                        new
                        {
                            email = Email,
                            userName = UserName,
                            role = Role
                        },
                        Keys.UsersCollection,
                        userId,
                        _ => { });
                    success = true;
                }
            }
            return success;
        }
        /// <summary>
        /// Validates email format.
        /// </summary>
        /// <returns>True if email is valid.</returns>
        private bool IsEmailValid()
        {
            bool isValid = Email.Contains('@') && Email.Contains('.');
            if (!isValid)
                Shell.Current.DisplayAlert(
                    Strings.RegisterErrorTitle,
                    Strings.EmailInvalid,
                    Strings.Ok);
            return isValid;
        }
        /// <summary>
        /// Validates email format for login.
        /// </summary>
        /// <returns>True if email is valid.</returns>
        private bool IsEmailValidLogin()
        {
            return !string.IsNullOrWhiteSpace(Email) && Email.Contains("@") && Email.Contains(".");
        }
        /// <summary>
        /// Validates password is not empty for login.
        /// </summary>
        /// <returns>True if password is not empty.</returns>
        private bool IsPasswordValidLogin()
        {
            return !string.IsNullOrWhiteSpace(Password);
        }
        /// <summary>
        /// Validates username length.
        /// </summary>
        /// <returns>True if username is valid.</returns>
        private bool IsUserNameValid()
        {
            bool isValid = UserName.Length >= ConstData.MinCharacterInUN;
            if (!isValid)
                Shell.Current.DisplayAlert(
                    Strings.RegisterErrorTitle,
                    Strings.UserNameTooShort,
                    Strings.Ok);
            return isValid;
        }
        /// <summary>
        /// Validates that a role is selected.
        /// </summary>
        /// <returns>True if role is valid.</returns>
        private bool IsRoleValid()
        {
            bool isValid = Role == Strings.Client || Role == Strings.Admin;
            if (!isValid)
                Shell.Current.DisplayAlert(
                    Strings.RegisterErrorTitle,
                    Strings.RoleNotSelected,
                    Strings.Ok);
            return isValid;
        }
        /// <summary>
        /// Validates password complexity.
        /// </summary>
        /// <returns>True if password meets requirements.</returns>
        private bool IsPasswordValid()
        {
            bool isValid = true;
            if (Password.Length < ConstData.MinCharacterInPW)
            {
                Shell.Current.DisplayAlert(
                    Strings.RegisterErrorTitle,
                    Strings.PasswordTooShort,
                    Strings.Ok);
                isValid = false;
            }
            else if (!HasUppercaseChar(Password))
            {
                Shell.Current.DisplayAlert(
                    Strings.RegisterErrorTitle,
                    Strings.PasswordNeedsUppercase,
                    Strings.Ok);
                isValid = false;
            }
            return isValid;
        }
        /// <summary>
        /// Checks if a string contains at least one uppercase character.
        /// </summary>
        /// <param name="text">The text to check.</param>
        /// <returns>True if text contains uppercase character.</returns>
        private static bool HasUppercaseChar(string text)
        {
            bool hasUpper = false;
            for (int i = 0; i < text.Length && !hasUpper; i++)
                if (text[i] >= 'A' && text[i] <= 'Z')
                    hasUpper = true;
            return hasUpper;
        }
        /// <summary>
        /// Handles login completion.
        /// </summary>
        /// <param name="task">The login task.</param>
        /// <returns>True if login completed successfully.</returns>
        private async Task<bool> OnCompleteLogin(Task task)
        {
            bool completedSuccessfully = task.IsCompletedSuccessfully;
            if (completedSuccessfully)
            {
                string userId = fbData.UserId;
                if (!string.IsNullOrEmpty(userId))
                    Preferences.Set(Keys.UserIdKey, userId);
                Preferences.Set(Keys.EmailKey, Email);
                await LoadUserRoleAsync();
            }
            return completedSuccessfully;
        }
        /// <summary>
        /// Loads user role from Firebase.
        /// </summary>
        private async Task LoadUserRoleAsync()
        {
            IDocumentSnapshot document =
                await fbData.fs
                    .Collection(Keys.UsersCollection)
                    .Document(fbData.UserId)
                    .GetAsync();
            if (document.Exists)
            {
                UserData? userData = document.ToObject<UserData>();
                if (userData != null)
                {
                    Role = userData.role;
                    UserName = userData.userName;
                }
            }
            Preferences.Set(Keys.UserNameKey, UserName);
        }
        #endregion
    }
}
