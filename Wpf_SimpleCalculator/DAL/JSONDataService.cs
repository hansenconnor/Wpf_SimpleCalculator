using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wpf_SimpleCalculator.Models;
using Newtonsoft.Json.Linq;

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


        public static JObject GetJsonResponse(string requestUrl)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("Authorization", "Bearer BQDqW6XTOcOpF1zNjcCDHVhXT7eCmuVMtO3ziGi3nWxFHrkmLs2Yehxk_7PkfqvqYDtUDZX4kC7x-gJxsfk");
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
