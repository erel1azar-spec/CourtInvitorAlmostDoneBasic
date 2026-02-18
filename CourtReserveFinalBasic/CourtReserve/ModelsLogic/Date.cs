using CourtReserve.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.ModelsLogic
{
    public class Date:DateModel
    {
        private readonly FbData fbData=new FbData();

        public override async Task LoadAsync()
        {
            string clubName = Preferences.Get(Keys.AdminSelectedClub, string.Empty);
            if (!string.IsNullOrWhiteSpace(clubName))
            {
                IQuerySnapshot snapshot = await fbData.fs.Collection(clubName).GetAsync();

                Dictionary<string, int> freePerDate = new Dictionary<string, int>();
                Dictionary<string, int> totalPerDate = new Dictionary<string, int>();

                foreach (IDocumentSnapshot document in snapshot.Documents)
                {
                    if (document.Data != null && document.Data.ContainsKey(Keys.Date))
                    {
                        string date = document.Get<string>(Keys.Date) ?? string.Empty;
                        if (!string.IsNullOrWhiteSpace(date))
                        {
                            if (!freePerDate.ContainsKey(date))
                            {
                                freePerDate[date] = 0;
                                totalPerDate[date] = 0;
                            }

                            if (document.Data.ContainsKey(Keys.LclientsField))
                            {
                                IList<Client>? clientsList = document.Get<IList<Client>>(Keys.LclientsField);
                                if (clientsList != null)
                                {
                                    totalPerDate[date] += clientsList.Count;
                                    foreach (Client c in clientsList)
                                    {
                                        if (c.UserId == string.Empty && c.Name == string.Empty)
                                            freePerDate[date]++;
                                    }
                                }
                            }
                        }
                    }
                }

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    dates.Clear();
                    foreach (string date in freePerDate.Keys)
                    {
                        dates.Add(new AdminExistingDatesText(date, freePerDate[date], totalPerDate[date]));
                    }
                });
            }
            else
            {
                await MainThread.InvokeOnMainThreadAsync(() => dates.Clear());
            }
        }
        public override async Task LoadAsyncClient()
        {
            string clubName = Preferences.Get(Keys.ClientSelectedClub, string.Empty);
            if (!string.IsNullOrWhiteSpace(clubName))
            {
                IQuerySnapshot snapshot = await fbData.fs.Collection(clubName).GetAsync();

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    dates.Clear();
                    foreach (IDocumentSnapshot document in snapshot.Documents)
                        AddDateFromDocument(document);

                    FilterFutureDates();
                });
            }
            else
            {
                await MainThread.InvokeOnMainThreadAsync(() => dates.Clear());
            }
        }
        public override void SelectDate(string date)
        {
            Preferences.Set(Keys.AdminSelectedDate, date);
        }
        public override void SelectDateClient(string date)
        {
            Preferences.Set(Keys.ClientSelectedDate, date);
        }
        protected override void AddDateFromDocument(IDocumentSnapshot document)
        {
            if (document.Data != null && document.Data.ContainsKey(Keys.Date))
            {
                string date = document.Get<string>(Keys.Date) ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(date) && !dates.Any(d => d.DateText == date))
                    dates.Add(new AdminExistingDatesText(date));
            }
        }

        protected void FilterFutureDates()
        {
            DateTime today = DateTime.Today;
            List<AdminExistingDatesTextModel> pastDates = new List<AdminExistingDatesTextModel>();

            foreach (AdminExistingDatesTextModel dateItem in dates)
            {
                if (DateTime.TryParseExact(dateItem.DateText, ConstData.DateFormat, 
                    System.Globalization.CultureInfo.InvariantCulture, 
                    System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    if (parsedDate < today)
                    {
                        pastDates.Add(dateItem);
                    }
                }
            }

            foreach (AdminExistingDatesTextModel pastDate in pastDates)
            {
                dates.Remove(pastDate);
            }

            List<AdminExistingDatesTextModel> sortedDates = dates
                .Select(d => new
                {
                    DateItem = d,
                    ParsedDate = DateTime.TryParseExact(d.DateText, ConstData.DateFormat,
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None, out DateTime dt) ? dt : DateTime.MaxValue
                })
                .OrderBy(x => x.ParsedDate)
                .Select(x => x.DateItem)
                .ToList();

            dates.Clear();
            foreach (AdminExistingDatesTextModel date in sortedDates)
            {
                dates.Add(date);
            }
        }
    }
}
