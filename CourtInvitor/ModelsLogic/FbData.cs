using CourtInvitor.Models;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Plugin.CloudFirestore;

namespace CourtInvitor.ModelsLogic
{
    
        public class FbData : FbDateModel
        {
            public override async Task<bool> CreateUserWithEmailAndPWAsync(string email, string password,String UserName, Func<Task, Task<bool>> OnCompleteRegister)
            {
                Task<Firebase.Auth.UserCredential> firebaseTask = facl.CreateUserWithEmailAndPasswordAsync(email, password);
                bool success = false;

                try
                {
                    UserCredential credential = await firebaseTask;
                    Firebase.Auth.User user = credential.User;

                    // Immediately sign in the new user so Firestore writes can succeed
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
            public override async Task<bool> SignInWithEmailAndPWdAsync(string email, string password, Func<Task, Task<bool>> OnCompleteLogin)
            {
                // Start Firebase sign-in
                Task<Firebase.Auth.UserCredential> firebaseTask = facl.SignInWithEmailAndPasswordAsync(email, password);
                bool success = false;

                try
                {
                    // Await Firebase sign-in
                    await firebaseTask;
                }
                catch (Exception ex)
                {
                    // Wrap the exception in a Task to pass to the callback
                    TaskCompletionSource<Firebase.Auth.UserCredential> tcs = new();
                    tcs.SetException(ex);
                    firebaseTask = tcs.Task;
                }
                finally
                {
                    // Always invoke the callback, even if the sign-in failed
                    success = await OnCompleteLogin(firebaseTask);
                }

                return success;
        }
        public override string SetDocument(object obj, string collectonName, string id, Action<System.Threading.Tasks.Task> OnComplete)
        {
            IDocumentReference dr = string.IsNullOrEmpty(id) ? fs.Collection(collectonName).Document() : fs.Collection(collectonName).Document(id);
            dr.SetAsync(obj).ContinueWith(OnComplete);
            return dr.Id;
        }


        public override void SignOut()
            {
                if (facl != null && facl.User != null)
                    facl.SignOut();
            }
        public override IListenerRegistration AddSnapshotListener(string collectonName, Plugin.CloudFirestore.QuerySnapshotHandler OnChange)
        {
            ICollectionReference cr = fs.Collection(collectonName);
            return cr.AddSnapshotListener(OnChange);
        }
        public override IListenerRegistration AddSnapshotListener(string collectonName, string id, Plugin.CloudFirestore.DocumentSnapshotHandler OnChange)
        {
            IDocumentReference cr = fs.Collection(collectonName).Document(id);
            return cr.AddSnapshotListener(OnChange);
        }
        public async void GetDocumentsWhereEqualTo(string collectonName, string fName, object fValue, Action<IQuerySnapshot> OnComplete)
        {
            ICollectionReference cr = fs.Collection(collectonName);
            IQuerySnapshot qs = await cr.WhereEqualsTo(fName, fValue).GetAsync();
            OnComplete(qs);
        }

    }
    
}
