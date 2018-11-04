using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wpf_SimpleCalculator.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.IO;
using Newtonsoft.Json;
using System.Configuration;
using System.Collections.Specialized;

namespace Wpf_SimpleCalculator.DAL
{
    public class JSONDataService : IDataService
    {
        // Return dynamic search object
        public dynamic getDynamicQueryResult( string searchQuery, ItemType type, int limit )
        {
            string _type = type.ToString();
            string _limit = limit.ToString();

                // TODO: Validate query before setting to requestUrl
                string requestUrl = string.Format("https://api.spotify.com/v1/search?q={0}&type={1}&limit={2}", searchQuery, _type, _limit);

            JObject response = GetJsonResponse(requestUrl);

            return response;
        }


        public dynamic getRecommendations(List<string> artistSeeds, string danceability, string energy, string popularity, string resultLimit)
        {
            string joined = string.Join(",", artistSeeds);

            string requestUrl = string.Format("https://api.spofity.com/v1/recommendations?seed_artists={0}&danceability={1}&energy={2}&popularity={3}&limit={4}", joined, danceability, energy, popularity, resultLimit);

            JObject response = GetJsonResponse(requestUrl);

            return response;
        }


        public string getAccessToken()
        {
            string requestUrl = "https://accounts.spotify.com/api/token";

            using (WebClient wc = new WebClient())
            {
                JObject responseObject;
                string accessToken = "";
                wc.Proxy = null;
                wc.Headers.Add("Authorization",
                    "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes("aa4f6bd22b774ab39682574973cb225f" + ":" + "38b86008f8bd48d493b33d255046d379")));
                NameValueCollection col = new NameValueCollection
                {
                    {"grant_type", "client_credentials"},
                };

                String response;
                try
                {
                    byte[] data = wc.UploadValues("https://accounts.spotify.com/api/token", "POST", col);
                    response = Encoding.UTF8.GetString(data);
                }
                catch (WebException e)
                {
                    using (StreamReader reader = new StreamReader(e.Response.GetResponseStream()))
                    {
                        response = reader.ReadToEnd();
                    }
                }
                responseObject = JsonConvert.DeserializeObject<JObject>(response);
                accessToken = responseObject["access_token"].ToString();

                return accessToken;
            }
        }


        public static JObject GetJsonResponse(string requestUrl)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    string accessToken = ConfigurationManager.AppSettings.Get("ACCESS_TOKEN");
                    // TODO: Makeu sure key value is not NULL
                    //To display the value that the application retrieves in the Console window, use
                    // TODO dyamically pull bearer token from app.config
                    string authValue = "Bearer" + " " + accessToken;
                    wc.Headers.Add("Authorization", authValue);
                    var json = wc.DownloadString(requestUrl);
                    JObject deserializedResponse = JObject.Parse(json);

                    return deserializedResponse;
                }
                // dynamic product = new JObject();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Console.Read();
                return null;
            }
        }
    }
}
