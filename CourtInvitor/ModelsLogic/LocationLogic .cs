using CourtInvitor.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CourtInvitor.ModelsLogic
{
    public class LocationLogic : LocationInputModel
    {
        private const string BaseUrl = Strings.LocationIP;

        public async Task<List<LocationResult>> GetSuggestionsAsync(string query)
        {
            List<LocationResult> results = new();
            if (string.IsNullOrWhiteSpace(query) || query.Length < 2)
                return results;

            using HttpClient client = new();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("CourtInvitorApp/1.0");

            string url = $"{BaseUrl}?q={query}&format=json&limit=5";
            string json = await client.GetStringAsync(url);

            List<NominatimResult>? data = JsonSerializer.Deserialize<List<NominatimResult>>(json);
            if (data != null)
            {
                foreach (var item in data)
                    results.Add(new LocationResult { DisplayName = item.display_name });
            }
            return results;
        }

        private class NominatimResult
        {
            public string display_name { get; set; }
        }

    }
}

