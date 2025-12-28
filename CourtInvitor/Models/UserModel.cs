using CourtInvitor.ModelsLogic;
namespace CourtInvitor.Models
{
    public abstract class UserModel
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public abstract Task<bool> Register();
        public abstract void SignOut();
        public abstract Task<bool> Login();
        public abstract bool CanRegister();
       
    }
}
