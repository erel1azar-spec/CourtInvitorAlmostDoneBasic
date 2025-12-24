using CourtInvitor.Models;

namespace CourtInvitor.ModelsLogic
{
    internal class UserProfile:UserProfileModel
    {
        public UserProfile()
        {
            FirstName = Preferences.Get(Keys.FirstNameKey, string.Empty);
            LastName = Preferences.Get(Keys.LastNameKey, string.Empty);
            City = Preferences.Get(Keys.CityKey, string.Empty);
        }
    }
}
