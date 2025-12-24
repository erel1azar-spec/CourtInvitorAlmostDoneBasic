using Firebase.Auth;
using Firebase.Auth.Providers;
using Plugin.CloudFirestore;

namespace CourtInvitor.Models
{
    public abstract class FbDateModel
    {
        protected FirebaseAuthClient facl;
        public IFirestore fs;
        //protected IFirestore fdb;
        public string DisplayName => facl != null && facl.User != null ? facl.User.Info.DisplayName : string.Empty;
        public string UserId => facl != null ? facl.User.Uid : string.Empty;
        public abstract Task<bool> CreateUserWithEmailAndPWAsync(string email, string password,String UserName, Func<Task, Task<bool>> OnCompleteRegister);
        public abstract string SetDocument(object obj, string collectonName, string id, Action<System.Threading.Tasks.Task> OnComplete);
        public abstract Task<bool> SignInWithEmailAndPWdAsync(string email, string password, Func<Task, Task<bool>> OnCompleteLogin);
        public abstract void SignOut();
        public abstract IListenerRegistration AddSnapshotListener(string collectonName, Plugin.CloudFirestore.QuerySnapshotHandler OnChange);
        public abstract IListenerRegistration AddSnapshotListener(string collectonName, string id, Plugin.CloudFirestore.DocumentSnapshotHandler OnChange);
        // 1) FbDataModel constructor — make sure GoogleProvider is included
        public FbDateModel()
        {
            FirebaseAuthConfig fac = new()
            {
                ApiKey = Keys.FbApiKey,
                AuthDomain = "courtinvitor.firebaseapp.com",
                Providers = [new EmailProvider()]
                // optionally set UserRepository if you want persistence
            };
            facl = new FirebaseAuthClient(fac);
            fs = CrossCloudFirestore.Current.Instance;
        }
    }
}
