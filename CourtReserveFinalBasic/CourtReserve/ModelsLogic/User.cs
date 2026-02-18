using CommunityToolkit.Mvvm.Messaging;
using CourtReserve.Models;
using Plugin.CloudFirestore;

namespace CourtReserve.ModelsLogic
{
    public class User:UserModel
    {
        private readonly FbData fbData = new FbData();
            public override bool CanRegister()
            {
                return IsEmailValid() && IsPasswordValid() && IsUserNameValid() && IsRoleValid();
            }
            public override async Task<bool> Register()
            {
                return await fbData.CreateUserWithEmailAndPWAsync(
                    Email,
                    Password,
                    UserName,
                    OnRegisterCompleted);
            }
            public override void SignOut()
            {
                fbData.SignOut();
                Preferences.Clear();
            }
            public override string GetNavigationRoute()
            {
                string route = "///NavigationPageClient";
                if (Role == Strings.Admin)
                    route = "///NavigationPageAdmin";
                return route;
            }
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
            protected override string IdentifyFirebaseError(Task task)
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
            protected override async Task<bool> OnRegisterCompleted(Task task)
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
                        Preferences.Set(Keys.UserNameKey, UserName);
                        success = true;
                    }
                }
                return success;
            }
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
            private bool IsEmailValidLogin()
            {
                return !string.IsNullOrWhiteSpace(Email) && Email.Contains("@") && Email.Contains(".");
            }
            private bool IsPasswordValidLogin()
            {
                return !string.IsNullOrWhiteSpace(Password);
            }
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
            private static bool HasUppercaseChar(string text)
            {
                bool hasUpper = false;
                for (int i = 0; i < text.Length && !hasUpper; i++)
                    if (text[i] >= 'A' && text[i] <= 'Z')
                        hasUpper = true;
                return hasUpper;
            }
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
        }
    }
