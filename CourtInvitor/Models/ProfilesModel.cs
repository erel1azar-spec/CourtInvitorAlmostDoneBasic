using CourtInvitor.ModelsLogic;
using Microsoft.Maui.Controls;
using Plugin.CloudFirestore;
using System.Collections.ObjectModel;


namespace CourtInvitor.Models
{
    internal class ProfilesModel
    {
       

        protected FbData fbd = new();
        protected IListenerRegistration? ilr;
        public ObservableCollection<Profile>? ProfileList { get; set; } = [];
        public bool IsBusy { get; set; }
        public ObservableCollection<Profile>? ProfileDatas { get; set; } = [];
        public EventHandler<bool>? OnProfileAdded;
        public EventHandler? OnProfilesChanged;
    }
}
