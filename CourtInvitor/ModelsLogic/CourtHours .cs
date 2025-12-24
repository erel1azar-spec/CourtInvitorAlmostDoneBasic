using CourtInvitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtInvitor.ModelsLogic
{
    internal class CourtHours: CourtHourModel
    {
        private readonly FbData fbData = new();

        public override string HourText { get; set; } = string.Empty;
        public override string ClientId { get; set; } = string.Empty;

        public override string DisplayText
        {
            get
            {
                return $"{HourText} - {ClientId}";
            }
        }

        public async Task<List<CourtHourModel>> LoadHoursAsync()
        {
            string date =
                Preferences.Get(Keys.SelectedDate, string.Empty);

            int court =
                Preferences.Get(Keys.SelectedCourt, 0);

            if (date == string.Empty || court == 0)
                return new List<CourtHourModel>();

            var snapshot = await fbData.fs
                .Collection("games")
                .GetAsync();

            foreach (var doc in snapshot.Documents)
            {
                if (doc.Data["date"].ToString() == date &&
                    int.Parse(doc.Data["court"].ToString()) == court)
                {
                    var clientsArray =
                        (IList<object>)doc.Data["clientsId"];

                    List<CourtHourModel> result = new();

                    for (int i = 0; i < clientsArray.Count; i++)
                    {
                        string clientId =
                            clientsArray[i]?.ToString() ?? string.Empty;

                        // אם פנוי – לא מוסיפים לרשימה בכלל
                        if (clientId == string.Empty)
                            continue;

                        result.Add(new CourtHours
                        {
                            HourText = $"{6 + i:00}:00",
                            ClientId = clientId
                        });
                    }

                    return result;
                }
            }

            return new List<CourtHourModel>();
        }
    }
}
