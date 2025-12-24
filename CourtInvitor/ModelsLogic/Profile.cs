using CommunityToolkit.Maui.Alerts;
using CourtInvitor.Models;

namespace CourtInvitor.ModelsLogic
{
    internal class Profile : ProfileModel
    {

        internal Profile()
        {
            FirstName = new UserProfile().FirstName;
            LastName = new UserProfile().LastName;
            City = new UserProfile().City;
            Created = DateTime.Now;
        }
        
        public override void SetDocument(Action<System.Threading.Tasks.Task> OnComplete)
        { 
            Id = fbd.SetDocument(this, Keys.Profiles, Id, OnComplete);
        }
    
    }
}
