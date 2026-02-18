using Firebase.Auth;
using Plugin.CloudFirestore;

namespace CourtInvitor.Models
{
    /// <summary>
    /// Firebase data operations implementation.
    /// </summary>
    public class FbData : FbDateModel
    {
        public override async Task<bool> CreateUserWithEmailAndPWAsync(string email, string password, string UserName, Func<Task, Task<bool>> OnCompleteRegister)
        {
            Task<UserCredential> firebaseTask = facl.CreateUserWithEmailAndPasswordAsync(email, password);
            bool success = false;
            try
            {
                UserCredential credential = await firebaseTask;
                await facl.SignInWithEmailAndPasswordAsync(email, password);
            }
            catch (Exception ex)
            {
                TaskCompletionSource<UserCredential> tcs = new();
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
            Task<UserCredential> firebaseTask = facl.SignInWithEmailAndPasswordAsync(email, password);
            bool success = false;
            try
            {
                await firebaseTask;
            }
            catch (Exception ex)
            {
                TaskCompletionSource<UserCredential> tcs = new();
                tcs.SetException(ex);
                firebaseTask = tcs.Task;
            }
            finally
            {
                success = await OnCompleteLogin(firebaseTask);
            }
            return success;
        }

        public override string SetDocument(object obj, string collectonName, string id, Action<Task> OnComplete)
        {
            IDocumentReference dr = string.IsNullOrEmpty(id) 
                ? fs.Collection(collectonName).Document() 
                : fs.Collection(collectonName).Document(id);
            dr.SetAsync(obj).ContinueWith(OnComplete);
            return dr.Id;
        }

        public override void SignOut()
        {
            if (facl?.User != null)
                facl.SignOut();
        }

        public override IListenerRegistration AddSnapshotListener(string collectonName, QuerySnapshotHandler OnChange)
        {
            ICollectionReference cr = fs.Collection(collectonName);
            return cr.AddSnapshotListener(OnChange);
        }

        public override IListenerRegistration AddSnapshotListener(string collectonName, string id, DocumentSnapshotHandler OnChange)
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

