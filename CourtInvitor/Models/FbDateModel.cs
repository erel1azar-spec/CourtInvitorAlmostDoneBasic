using Firebase.Auth;
using Firebase.Auth.Providers;
using Plugin.CloudFirestore;
namespace CourtInvitor.Models
{
    /// <summary>
    /// Abstract model for Firebase data operations.
    /// </summary>
    public abstract class FbDateModel
    {
        #region Fields
        /// <summary>
        /// Firebase authentication client.
        /// </summary>
        protected FirebaseAuthClient facl;
        /// <summary>
        /// Firestore instance.
        /// </summary>
        public IFirestore fs;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the current user's display name.
        /// </summary>
        public string DisplayName => facl != null && facl.User != null ? facl.User.Info.DisplayName : string.Empty;
        /// <summary>
        /// Gets the current user's ID.
        /// </summary>
        public string UserId => facl?.User?.Uid ?? string.Empty;
        #endregion
        #region Abstract Methods
        /// <summary>
        /// Creates a new user with email and password.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <param name="password">User password.</param>
        /// <param name="UserName">User display name.</param>
        /// <param name="OnCompleteRegister">Callback on completion.</param>
        /// <returns>True if registration succeeded.</returns>
        public abstract Task<bool> CreateUserWithEmailAndPWAsync(string email, string password, String UserName, Func<Task, Task<bool>> OnCompleteRegister);
        /// <summary>
        /// Sets a document in Firestore.
        /// </summary>
        /// <param name="obj">The object to store.</param>
        /// <param name="collectonName">Collection name.</param>
        /// <param name="id">Document ID.</param>
        /// <param name="OnComplete">Callback on completion.</param>
        /// <returns>The document ID.</returns>
        public abstract string SetDocument(object obj, string collectonName, string id, Action<System.Threading.Tasks.Task> OnComplete);
        /// <summary>
        /// Signs in with email and password.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <param name="password">User password.</param>
        /// <param name="OnCompleteLogin">Callback on completion.</param>
        /// <returns>True if login succeeded.</returns>
        public abstract Task<bool> SignInWithEmailAndPWdAsync(string email, string password, Func<Task, Task<bool>> OnCompleteLogin);
        /// <summary>
        /// Signs out the current user.
        /// </summary>
        public abstract void SignOut();
        /// <summary>
        /// Adds a collection snapshot listener.
        /// </summary>
        /// <param name="collectonName">Collection name.</param>
        /// <param name="OnChange">Change handler.</param>
        /// <returns>The listener registration.</returns>
        public abstract IListenerRegistration AddSnapshotListener(string collectonName, Plugin.CloudFirestore.QuerySnapshotHandler OnChange);
        /// <summary>
        /// Adds a document snapshot listener.
        /// </summary>
        /// <param name="collectonName">Collection name.</param>
        /// <param name="id">Document ID.</param>
        /// <param name="OnChange">Change handler.</param>
        /// <returns>The listener registration.</returns>
        public abstract IListenerRegistration AddSnapshotListener(string collectonName, string id, Plugin.CloudFirestore.DocumentSnapshotHandler OnChange);
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the FbDateModel class.
        /// </summary>
        public FbDateModel()
        {
            FirebaseAuthConfig fac = new()
            {
                ApiKey = Keys.FbApiKey,
                AuthDomain = "courtinvitor.firebaseapp.com",
                Providers = [new EmailProvider()]
            };
            facl = new FirebaseAuthClient(fac);
            fs = CrossCloudFirestore.Current.Instance;
        }
        #endregion
    }
}
