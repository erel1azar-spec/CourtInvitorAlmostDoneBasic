using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourtInvitor.ModelsLogic;

namespace CourtInvitor.ViewModels
{
    internal class SessionVM:ObservableObject
    {
        private  Session session;

        public string TimeLeft => session.TimeLeft;

        public SessionVM()
        {
            session = new Session();
            session.TimeLeftChanged += (_, _) =>
                OnPropertyChanged(nameof(TimeLeft));

            session.SessionExpired += async (_, _) =>
            {
                session = null;
                new User().SignOut();
                await Shell.Current.GoToAsync("///LoginPage");
            };
        }
    }
}
