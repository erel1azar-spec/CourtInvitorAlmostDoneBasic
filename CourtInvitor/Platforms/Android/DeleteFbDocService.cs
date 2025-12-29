using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using CourtInvitor.Models;
using Javax.Security.Auth;

namespace CourtInvitor.Platforms.Android
{
    public class DeleteFbDocService : Service
    {
        private bool isRunning = true;
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent? intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            ThreadStart threadStart = new ThreadStart(DeleteFbDocs);
            Thread thread = new Thread(threadStart);
            thread.Start();
            return base.OnStartCommand(intent, flags, startId);
        }
        private void DeleteFbDocs()
        {
            while (isRunning)
            {

                Thread.Sleep(Keys.OneHourIntMiliseconds); 
            }
            StopSelf();
        }
        public override IBinder? OnBind(Intent? intent)
        {
            //not used
            return null;
        }
        public override void OnDestroy()
        {
            isRunning = false;
            base.OnDestroy();
        }
    }
}
