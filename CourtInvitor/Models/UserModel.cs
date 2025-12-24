using CourtInvitor.ModelsLogic;
namespace CourtInvitor.Models
{
    public abstract class UserModel
    {

        // Add more fields as needed (createdAt, profilePic, etc.)

        protected FbData fbd = new();
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        //public abstract Task<bool> LoginWithGoogle();
        public abstract Task<bool> Login();
        public abstract Task<bool> Register();
        public abstract void SignOut();
        public abstract bool CanLogin();
        public abstract bool CanRegister();
       
    }
}
