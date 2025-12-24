using CommunityToolkit.Maui.Alerts;
using Plugin.CloudFirestore;
using CourtInvitor.Models;

namespace CourtInvitor.ModelsLogic
{
    internal class Profiles: ProfilesModel
    {
        internal void AddProfile()
        {
            IsBusy = true;
            Profile profile = new();
            profile.SetDocument(OnComplete);
        }
        private void OnComplete(Task task)
        {
            IsBusy = false;
            OnProfileAdded?.Invoke(this, task.IsCompletedSuccessfully);
        }
        public Profiles()
        {

        }
        public void AddSnapshotListener()
        {
            ilr = fbd.AddSnapshotListener(Keys.ProfilesCollection, OnChange!);
        }
        public void RemoveSnapshotListener()
        {
            ilr?.Remove();
        }
        private void OnChange(IQuerySnapshot snapshot, Exception error)
        {
            fbd.GetDocumentsWhereEqualTo(Keys.ProfilesCollection, nameof(ProfileModel), false, OnComplete);
        }

        private void OnComplete(IQuerySnapshot qs)
        {
            ProfileList!.Clear();
            foreach (IDocumentSnapshot ds in qs.Documents)
            {
                Profile? profile = ds.ToObject<Profile>();
                if (profile != null)
                {
                    profile.Id = ds.Id;
                    ProfileList.Add(profile);
                }
            }
            OnProfilesChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
