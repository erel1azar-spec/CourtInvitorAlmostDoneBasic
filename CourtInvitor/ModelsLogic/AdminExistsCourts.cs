using CourtInvitor.Models;
using Plugin.CloudFirestore;

namespace CourtInvitor.ModelsLogic
{
    /// <summary>
    /// Implementation of admin courts retrieval logic.
    /// </summary>
    internal class AdminExistsCourts : AdminExistsCourtsModel
    {
        #region Fields
        private int courtNumber;
        private string courtText = string.Empty;
        #endregion
        #region Properties
        public override int CourtNumber => courtNumber;
        public override string CourtText => courtText;
        #endregion
        #region Public Functions
        /// <summary>
        /// Loads courts for a specific date.
        /// </summary>
        /// <param name="clubName">The club name.</param>
        /// <param name="date">The selected date.</param>
        /// <returns>List of courts for the date.</returns>
        public static async Task<List<AdminExistsCourtsModel>> LoadCourtsForDateAsync(string clubName, string date)
        {
            List<AdminExistsCourtsModel> courts = new();
            if (string.IsNullOrEmpty(clubName) || string.IsNullOrEmpty(date))
            {
                courts = new List<AdminExistsCourtsModel>();
            }
            else
            {
                FbData data = new FbData();
                IQuerySnapshot snapshot = await data.fs.Collection(clubName).WhereEqualsTo(Keys.Date, date).GetAsync();
                foreach (IDocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Data != null && document.Data.ContainsKey(Keys.CourtNumber))
                    {
                        int number = document.Get<int>(Keys.CourtNumber);
                        if (number > 0)
                        {
                            bool exists = courts.Any(c => c.CourtNumber == number);
                            if (!exists)
                            {
                                AdminExistsCourts model = new()
                                {
                                    courtNumber = number,
                                    courtText = Strings.Court + number
                                };
                                courts.Add(model);
                            }
                        }
                    }
                }
            }
            return courts;
        }
        /// <summary>
        /// Loads all courts for a club.
        /// </summary>
        /// <param name="clubName">The club name.</param>
        /// <returns>List of all courts.</returns>
        public static async Task<List<AdminExistsCourtsModel>> LoadCourtsAsync(string clubName)
        {
            List<AdminExistsCourtsModel> courts = new();
            if (!string.IsNullOrEmpty(clubName))
            {
                FbData data = new FbData();
                IQuerySnapshot snapshot = await data.fs.Collection(clubName).GetAsync();
                foreach (IDocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Data != null && document.Data.ContainsKey(Keys.CourtNumber))
                    {
                        int number = document.Get<int>(Keys.CourtNumber);
                        if (number > 0)
                        {
                            bool exists = courts.Any(c => c.CourtNumber == number);
                            if (!exists)
                            {
                                AdminExistsCourts model = new()
                                {
                                    courtNumber = number,
                                    courtText = Strings.Court + number
                                };
                                courts.Add(model);
                            }
                        }
                    }
                }
            }
            return courts;
        }
        #endregion
    }
}
