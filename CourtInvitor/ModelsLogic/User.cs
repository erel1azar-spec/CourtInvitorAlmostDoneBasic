using CourtInvitor.Models;
using Plugin.CloudFirestore;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using static CourtInvitor.Models.ConstData;


namespace CourtInvitor.ModelsLogic
{
    public class User : UserModel
    {
        private readonly FbData fbData;
        public User()
        {
            fbData = new FbData();
        }
        private static string IdentifyFirebaseError(Task task)
        {
            Exception? exception = task.Exception?.InnerException;

            if (exception == null)
                return Strings.RegisterFailed;

            string message = exception.Message;

            if (message.Contains("EMAIL_EXISTS"))
                return Strings.EmailExistsError;

            if (message.Contains("WEAK_PASSWORD"))
                return Strings.WeakPasswordError;

            if (message.Contains("INVALID_EMAIL"))
                return Strings.EmailInvalid;

            if (message.Contains("TOO_MANY_ATTEMPTS"))
                return Strings.ManyAttemptsError;

            return Strings.RegisterFailed;
        }

        public override bool CanRegister()
        {
            return IsEmailValid() &&
                   IsPasswordValid() &&
                   IsUserNameValid();
        }

        public override async Task<bool> Register()
        {
            return await fbData.CreateUserWithEmailAndPWAsync(
                Email,
                Password,
                UserName,
                OnRegisterCompleted);
        }

        private async Task<bool> OnRegisterCompleted(Task task)
        {
            if (!task.IsCompletedSuccessfully)
            {
                string message = IdentifyFirebaseError(task);

                await Shell.Current.DisplayAlert(
                    Strings.RegisterErrorTitle,
                    message,
                    Strings.Ok);

                return false;
            }

            string userId = fbData.UserId;

            if (string.IsNullOrEmpty(userId))
                return false;

            fbData.SetDocument(
                new
                {
                    email = Email,
                    userName = UserName,
                    role = Role
                },
                "users",
                userId,
                _ => { });


            return true;
        }

        private bool IsEmailValid()
        {
            if (!Email.Contains('@') || !Email.Contains('.'))
            {
                Shell.Current.DisplayAlert(
                    Strings.RegisterErrorTitle,
                    Strings.EmailInvalid,
                    Strings.Ok);
                return false;
            }

            return true;
        }
        private bool IsUserNameValid()
        {
            if (UserName.Length < 3)
            {
                Shell.Current.DisplayAlert(
                    Strings.RegisterErrorTitle,
                    Strings.UserNameTooShort,
                    Strings.Ok);
                return false;
            }

            return true;
        }

        private bool IsPasswordValid()
        {
            if (Password.Length < 8)
                return false;

            bool hasUpper = false;
            bool hasLower = false;
            bool hasDigit = false;

            for (int i = 0; i < Password.Length; i++)
            {
                char c = Password[i];

                if (c >= 'A' && c <= 'Z')
                    hasUpper = true;
                else if (c >= 'a' && c <= 'z')
                    hasLower = true;
                else if (c >= '0' && c <= '9')
                    hasDigit = true;
            }

            return hasUpper && hasLower && hasDigit;
        }
        public override void SignOut()
        {
            fbData.SignOut();
            Preferences.Clear();
        }

        private bool IsEmailValidLogin()
        {
            return !string.IsNullOrWhiteSpace(Email) && Email.Contains("@") && Email.Contains(".");
        }

        private bool IsPasswordValidLogin()
        {
            if (Password.Length < 8)
                return false;

            bool hasUpper = false;
            bool hasLower = false;
            bool hasDigit = false;

            for (int i = 0; i < Password.Length; i++)
            {
                char c = Password[i];
                if (c >= 'A' && c <= 'Z')
                    hasUpper = true;
                else if (c >= 'a' && c <= 'z')
                    hasLower = true;
                else if (c >= '0' && c <= '9')
                    hasDigit = true;
            }

            return hasUpper && hasLower && hasDigit;
        }
        public override async Task<bool> Login()
        {
            bool loginSucceeded = false;

            if (IsEmailValidLogin() && IsPasswordValidLogin())
            {
                try
                {
                    bool signInResult =
                        await fbData.SignInWithEmailAndPWdAsync(
                            Email,
                            Password,
                            OnCompleteLogin);

                    loginSucceeded = signInResult;
                }
                catch
                {
                    loginSucceeded = false;
                }
            }

            return loginSucceeded;
        }
        private async Task<bool> OnCompleteLogin(Task task)
        {
            bool completedSuccessfully = task.IsCompletedSuccessfully;

            if (completedSuccessfully)
            {
                string userId = fbData.UserId;

                if (!string.IsNullOrEmpty(userId))
                    Preferences.Set(Keys.UserIdKey, userId);

                await LoadUserRoleAsync();
            }

            return completedSuccessfully;
        }
        private async Task LoadUserRoleAsync()
        {
            IDocumentSnapshot document =
                await fbData.fs
                    .Collection("users")
                    .Document(fbData.UserId)
                    .GetAsync();

            if (document.Exists)
            {
                UserData? userData = document.ToObject<UserData>();
                if (userData != null)
                {
                    Role = userData.role;
                    UserName = userData.username;
                }
            }
            Preferences.Set(Keys.UserNameKey, UserName);
        }


        //Strings dynamicStrings =new();
        //private string IdentifyFireBaseError(Task task)
        //{
        //    Exception? ex = task.Exception?.InnerException;
        //    string errorMessage = string.Empty;

        //    if (ex != null)
        //    {
        //        try
        //        {
        //            // Find the "Response:" part
        //            int responseIndex = ex.Message.IndexOf("Response:");
        //            if (responseIndex >= 0)
        //            {
        //                // Take everything after "Response:"
        //                string jsonPart = ex.Message.Substring(responseIndex + "Response:".Length).Trim();

        //                // Some Firebase responses might have extra closing braces, remove trailing stuff
        //                int lastBrace = jsonPart.LastIndexOf('}');
        //                if (lastBrace >= 0)
        //                    jsonPart = jsonPart.Substring(0, lastBrace + 1);

        //                // Parse JSON
        //                JsonDocument json = JsonDocument.Parse(jsonPart);

        //                JsonElement errorElem = json.RootElement.GetProperty("error");
        //                string firebaseMessage = errorElem.GetProperty("message").ToString();

        //                errorMessage = firebaseMessage switch
        //                {
        //                    Keys.EmailExistsErrorKey => Strings.EmailExistsError,
        //                    Keys.OperationNotAllowedErrorKey => Strings.OperationNotAllowedError,
        //                    Keys.WeakPasswordErrorKey => Strings.WeakPasswordError,
        //                    Keys.MissingEmailErrorKey => Strings.MissingEmailError,
        //                    Keys.MissingPasswordErrorKey => Strings.MissingPasswordError,
        //                    Keys.InvalidEmailErrorKey => Strings.InvalidEmailError,
        //                    Keys.InvalidCredentialsErrorKey => Strings.InvalidCredentialsError,
        //                    Keys.UserDisabledErrorKey => Strings.UserDisabledError,
        //                    Keys.ManyAttemptsErrorKey => Strings.ManyAttemptsError,
        //                    _ => Strings.DefaultRegisterError,
        //                };
        //            }
        //        }
        //        catch
        //        {
        //            errorMessage = Strings.FailedJsonError;
        //        }
        //    }
        //    return errorMessage;
        //}

        //public override async Task<bool> Login()
        //{
        //    bool success = await fbd.SignInWithEmailAndPWdAsync(Email, Password, async (task) =>
        //    {
        //        if (task.IsCompletedSuccessfully)
        //        {
        //            string uid = fbd.UserId;
        //            if (!string.IsNullOrEmpty(uid))
        //            {
        //                var snapshot = await fbd.fs.Collection("users").Document(uid).GetAsync();
        //                if (snapshot.Exists)
        //                {
        //                    var data = snapshot.ToObject<UserData>();
        //                    UserName = data.username;
        //                    Role = data.role;
        //                }
        //            }
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    });
        //    Preferences.Set(Keys.EmailKey, Email);
        //    Preferences.Set(Keys.UserIdKey, fbd.UserId);
        //    return success;
        //}
        //private async Task<bool> OnCompleteLogin(Task task)
        //{
        //    if (task.IsCompletedSuccessfully)
        //    {
        //        // UID של המשתמש הנוכחי
        //        string uid = fbd.UserId; // <-- השתמש ב-fbd.UserId במקום facl.User?.Uid

        //        if (!string.IsNullOrEmpty(uid))
        //        {
        //            // שליפת document מה-Firestore לפי UID
        //            var docRef = fbd.fs.Collection("users").Document(uid); // <-- השתמש ב-fbd.fs
        //            var snapshot = await docRef.GetAsync();

        //            if (snapshot.Exists)
        //            {
        //                var data = snapshot.ToObject<UserData>();
        //                UserName = data.username;
        //                Role = data.role;

        //                // ניווט לפי Role
        //                if (Role == Strings.Admin)
        //                    await Shell.Current.GoToAsync("///AdminHomePage");
        //                else
        //                    await Shell.Current.GoToAsync("///CustomerHomePage");
        //            }
        //        }

        //        return true;
        //    }
        //    else
        //    {
        //        string errorMessage = IdentifyFireBaseError(task);
        //        await Shell.Current.DisplayAlert("Login failed", errorMessage, "OK");
        //        return false;
        //    }
        //}
        //private void LoginSaveToPreferencesAsync()
        //{
        //    Preferences.Set(Keys.EmailKey, Email);
        //    Preferences.Set(Keys.PasswordKey, Password);
        //}
        //public override async Task<bool> Register()
        //{
        //    //bool success = await fbd.CreateUserWithEmailAndPWAsync(Email, Password, UserName, async (task) =>
        //    //{
        //    //    if (task.IsCompletedSuccessfully)
        //    //    {
        //    //        // שמירת Role ב־Firestore לפי UID
        //    //        string uid = fbd.UserId;
        //    //        if (!string.IsNullOrEmpty(uid))
        //    //        {
        //    //            await fbd.fs.Collection("users").Document(uid)
        //    //                .SetAsync(new UserData { username = UserName, role = Role });
        //    //        }
        //    //        return true;
        //    //    }
        //    //    else
        //    //    {
        //    //        return false;
        //    //    }
        //    //});


        //    RegisterSaveToPreferences();

        //    bool success = await fbd.CreateUserWithEmailAndPWAsync(Email, Password, UserName, OnCompleteRegister);

        //    return success;
        //    //return success;
        //}
        //private async Task<bool> OnCompleteRegister(Task task)
        //{
        //    if (task.IsCompletedSuccessfully)
        //    {
        //        string uid = fbd.UserId; // UID מה-Firebase Authentication
        //        if (!string.IsNullOrEmpty(uid))
        //        {
        //            // צור document ב-Firestore לפי UID
        //            var docObj = new
        //            {
        //                username = UserName,
        //                email = Email,
        //                role = Role // role כבר שמור באובייקט User
        //            };

        //            fbd.SetDocument(docObj, "users", uid, t =>
        //            {
        //                if (t.IsCompletedSuccessfully)
        //                    Debug.WriteLine("User document created in Firestore");
        //            });
        //        }

        //        await Shell.Current.DisplayAlert("Success", "Registration completed", "OK");
        //        return true;
        //    }
        //    else
        //    {
        //        string errorMessage = IdentifyFireBaseError(task);
        //        await Shell.Current.DisplayAlert("Error", errorMessage, "OK");
        //        return false;
        //    }

        //}
        //private void RegisterSaveToPreferences()
        //{
        //    Preferences.Set(Keys.UserNameKey, UserName);
        //    Preferences.Set(Keys.EmailKey, Email);
        //    Preferences.Set(Keys.PasswordKey, Password);
        //}
        //public override void SignOut()
        //{
        //    fbd.SignOut();
        //    Preferences.Clear();
        //}

        //public override bool CanLogin()
        //{
        //    return IsEmailValid() && IsPasswordValid();
        //}
        //public override bool CanRegister()
        //{
        //    bool nameVaild = IsUserNameValid();
        //    bool pwVaild = IsPasswordValid();
        //    bool emailVaild = IsEmailValid();

        //    return nameVaild && pwVaild && emailVaild;
        //    //return IsUserNameValid() && IsPasswordValid() && IsEmailValid();
        //}
        //private bool IsEmailValid()
        //{
        //    if (Email.Length < MinCharacterInEmail)
        //    {
        //        Shell.Current.DisplayAlert(Strings.EmailShortErrorTitle, dynamicStrings.EmailShortErrorMessage, Strings.EmailShortErrorButton);
        //        return false;
        //    }
        //    if (!HasAtSign(Email) || !HasDot(Email))
        //    {
        //        Shell.Current.DisplayAlert(Strings.EmailInvalidErrorTitle, Strings.EmailInvalidErrorMessage, Strings.EmailInvalidErrorButton);
        //        return false;
        //    }
        //    return true;
        //}
        //private bool IsPasswordValid()
        //{
        //    if (Password.Length < MinCharacterInPW)
        //    {
        //        Shell.Current.DisplayAlert(Strings.PasswordShortErrorTitle, dynamicStrings.PasswordShortErrorMessage, Strings.PasswordShortErrorButton);
        //        return false;
        //    }
        //    if (!HasNumber(Password))
        //    {
        //        Shell.Current.DisplayAlert(Strings.PasswordNumberErrorTitle, Strings.PasswordNumberErrorMessage, Strings.PasswordNumberErrorButton);
        //        return false;
        //    }
        //    if (!HasLowerCase(Password))
        //    {
        //        Shell.Current.DisplayAlert(Strings.PasswordLowerCaseErrorTitle, Strings.PasswordLowerCaseErrorMessage, Strings.PasswordLowerCaseErrorButton);
        //        return false;
        //    }
        //    if (!HasUpperCase(Password))
        //    {
        //        Shell.Current.DisplayAlert(Strings.PasswordUpperCaseErrorTitle, Strings.PasswordUpperCaseErrorMessage, Strings.PasswordUpperCaseErrorButton);
        //        return false;
        //    }
        //    return true;
        //}
        //private bool IsUserNameValid()
        //{
        //    if (UserName.Length < MinCharacterInUN || !HasNumber(UserName))
        //        return false;
        //    return true;
        //}
        //private static bool HasAtSign(string str)
        //{
        //    for (int i = 0; i < str.Length; i++)
        //        if (str[i] == '@')
        //            return true;
        //    return false;
        //}
        //private static bool HasDot(string str)
        //{
        //    for (int i = 0; i < str.Length; i++)
        //        if (str[i] == '.')
        //            return true;
        //    return false;
        //}
        //private static bool HasNumber(string str)
        //{
        //    for (int i = 0; i < str.Length; i++)
        //        if (str[i] >= '0' && str[i] <= '9')
        //            return true;
        //    return false;
        //}
        //private static bool HasLowerCase(string str)
        //{
        //    for (int i = 0; i < str.Length; i++)
        //        if (str[i] >= 'a' && str[i] <= 'z')
        //            return true;
        //    return false;
        //}
        //private static bool HasUpperCase(string str)
        //{
        //    for (int i = 0; i < str.Length; i++)
        //        if (str[i] >= 'A' && str[i] <= 'Z')
        //            return true;
        //    return false;
        //}


    }
}
