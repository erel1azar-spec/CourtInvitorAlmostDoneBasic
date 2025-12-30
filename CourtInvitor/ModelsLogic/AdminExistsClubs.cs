using CourtInvitor.Models;
using Plugin.CloudFirestore;
namespace CourtInvitor.ModelsLogic
{
    /// <summary>
    /// Implementation of admin club retrieval logic.
    /// </summary>
    internal class AdminExistsClubs : AdminExistsClubsModel
    {
        #region Fields
        private readonly FbData fbData;
        private string name;
        private string userEmail;
        #endregion
        #region Properties
        public override string Name => name;
        public override string UserEmail => userEmail;
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the AdminExistsClubs class.
        /// </summary>
        public AdminExistsClubs()
        {
            fbData = new FbData();
            name = string.Empty;
            userEmail = string.Empty;
        }
        #endregion
        #region Public Functions
        /// <summary>
        /// Loads a club by the user's email address.
        /// </summary>
        /// <param name="email">The user's email address.</param>
        public async Task LoadByUserEmailAsync(string email)
        {
            name = string.Empty;
            userEmail = string.Empty;
            IDocumentSnapshot? document = await FindClubByEmailAsync(email);
            if (document != null)
                ExtractClubData(document, email);
        }
        #endregion
        #region Private Functions
        /// <summary>
        /// Finds a club by user email.
        /// </summary>
        /// <param name="email">The email to search for.</param>
        /// <returns>The document snapshot if found.</returns>
        private async Task<IDocumentSnapshot?> FindClubByEmailAsync(string email)
        {
            IQuerySnapshot? snapshot =
                await fbData.fs.Collection(ConstData.Clubs).WhereEqualsTo(Keys.UserEmail, email).GetAsync();
            return snapshot.Documents.FirstOrDefault();
        }
        /// <summary>
        /// Extracts club data from the document.
        /// </summary>
        /// <param name="document">The Firestore document.</param>
        /// <param name="email">The fallback email.</param>
        private void ExtractClubData(IDocumentSnapshot document, string email)
        {
            string? tempName = document.Get<string>(Keys.Name);
            string? tempEmail = document.Get<string>(Keys.UserEmail);
            if (tempName != null)
                name = tempName;
            if (tempEmail != null)
                userEmail = tempEmail;
            else
                userEmail = email;
        }
        #endregion
    }
}
