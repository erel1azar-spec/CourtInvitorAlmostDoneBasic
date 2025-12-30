using CommunityToolkit.Mvvm.Messaging.Messages;

namespace CourtInvitor.Models
{
    public class AppMessage<T>(T msg) : ValueChangedMessage<T>(msg)
    {

    }
}
