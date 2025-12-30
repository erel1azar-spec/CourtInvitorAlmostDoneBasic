using CourtInvitor.Models;
using Firebase.Auth;
using Plugin.CloudFirestore;
namespace CourtInvitor.ModelsLogic
{
    /// <summary>
    /// Implementation of Firebase data operations.
    /// </summary>
    public class FbData : FbDateModel
    {
        #region Public Functions
        /// <summary>
        /// Creates a new user with email and password.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <param name="password">User password.</param>
        /// <param name="UserName">User display name.</param>
        /// <param name="OnCompleteRegister">Callback on completion.</param>
        /// <returns>True if registration succeeded.</returns>
        public override async Task<bool> CreateUserWithEmailAndPWAsync(string email, string password, String UserName, Func<Task, Task<bool>> OnCompleteRegister)
        {
            Task<Firebase.Auth.UserCredential> firebaseTask = facl.CreateUserWithEmailAndPasswordAsync(email, password);
            bool success = false;
            try
            {
                UserCredential credential = await firebaseTask;
                Firebase.Auth.User user = credential.User;
                await facl.SignInWithEmailAndPasswordAsync(email, password);
            }
            catch (Exception ex)
            {
                TaskCompletionSource<Firebase.Auth.UserCredential> tcs = new();
                tcs.SetException(ex);
                firebaseTask = tcs.Task;
            }
            finally
            {
                success = await OnCompleteRegister(firebaseTask);
            }
            return success;
        }
        /// <summary>
        /// Signs in with email and password.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <param name="password">User password.</param>
        /// <param name="OnCompleteLogin">Callback on completion.</param>
        /// <returns>True if login succeeded.</returns>
        public override async Task<bool> SignInWithEmailAndPWdAsync(string email, string password, Func<Task, Task<bool>> OnCompleteLogin)
        {
            Task<Firebase.Auth.UserCredential> firebaseTask = facl.SignInWithEmailAndPasswordAsync(email, password);
            bool success = false;
            try
            {
                await firebaseTask;
            }
            catch (Exception ex)
            {
                TaskCompletionSource<Firebase.Auth.UserCredential> tcs = new();
                tcs.SetException(ex);
                firebaseTask = tcs.Task;
            }
            finally
            {
                success = await OnCompleteLogin(firebaseTask);
            }
            return success;
        }
        /// <summary>
        /// Sets a document in Firestore.
        /// </summary>
        /// <param name="obj">The object to store.</param>
        /// <param name="collectonName">Collection name.</param>
        /// <param name="id">Document ID.</param>
        /// <param name="OnComplete">Callback on completion.</param>
        /// <returns>The document ID.</returns>
        public override string SetDocument(object obj, string collectonName, string id, Action<System.Threading.Tasks.Task> OnComplete)
        {
            IDocumentReference dr = string.IsNullOrEmpty(id) ? fs.Collection(collectonName).Document() : fs.Collection(collectonName).Document(id);
            dr.SetAsync(obj).ContinueWith(OnComplete);
            return dr.Id;
        }
        /// <summary>
        /// Signs out the current user.
        /// </summary>
        public override void SignOut()
        {
            if (facl != null && facl.User != null)
                facl.SignOut();
        }
        /// <summary>
        /// Adds a collection snapshot listener.
        /// </summary>
        /// <param name="collectonName">Collection name.</param>
        /// <param name="OnChange">Change handler.</param>
        /// <returns>The listener registration.</returns>
        public override IListenerRegistration AddSnapshotListener(string collectonName, Plugin.CloudFirestore.QuerySnapshotHandler OnChange)
        {
            ICollectionReference cr = fs.Collection(collectonName);
            return cr.AddSnapshotListener(OnChange);
        }
        /// <summary>
        /// Adds a document snapshot listener.
        /// </summary>
        /// <param name="collectonName">Collection name.</param>
        /// <param name="id">Document ID.</param>
        /// <param name="OnChange">Change handler.</param>
        /// <returns>The listener registration.</returns>
        public override IListenerRegistration AddSnapshotListener(string collectonName, string id, Plugin.CloudFirestore.DocumentSnapshotHandler OnChange)
        {
            IDocumentReference cr = fs.Collection(collectonName).Document(id);
            return cr.AddSnapshotListener(OnChange);
        }
        /// <summary>
        /// Gets documents matching a field value.
        /// </summary>
        /// <param name="collectonName">Collection name.</param>
        /// <param name="fName">Field name.</param>
        /// <param name="fValue">Field value.</param>
        /// <param name="OnComplete">Completion callback.</param>
        public async void GetDocumentsWhereEqualTo(string collectonName, string fName, object fValue, Action<IQuerySnapshot> OnComplete)
        {
            ICollectionReference cr = fs.Collection(collectonName);
            IQuerySnapshot qs = await cr.WhereEqualsTo(fName, fValue).GetAsync();
            OnComplete(qs);
        }
        #endregion
    }
}
