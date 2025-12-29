using CommunityToolkit.Mvvm.Messaging;
using CourtInvitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtInvitor.ModelsLogic
{
    internal class Session:SessionModel
    {
        public override string TimeLeft { get; protected set; } = string.Empty;

        public event EventHandler? TimeLeftChanged;
        public event EventHandler? SessionExpired;

        public Session()
        {
            RegisterTimer();
        }

        public override void RegisterTimer()
        {
            WeakReferenceMessenger.Default.Register<AppMessage<long>>(this, (r, m) =>
            {
                OnMessageReceived(m.Value);
            });
        }

        private void OnMessageReceived(long value)
        {
            if (value == Keys.FinishedSignal)
            {
                TimeLeft = Strings.TimeUp;
                SessionExpired?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                TimeLeft = TimeSpan
                    .FromMilliseconds(value)
                    .ToString(@"mm\:ss");

                TimeLeftChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
