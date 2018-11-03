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


        public string getAccessToken()
        {
            JObject responseObject;
            string requestUrl = "https://accounts.spotify.com/api/token";


            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
                var postData = "grant_type=client_credentials";
                byte[] data = Encoding.ASCII.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Add("Authorization", "Basic YWE0ZjZiZDIyYjc3NGFiMzk2ODI1NzQ5NzNjYjIyNWY6MzhiODYwMDhmOGJkNDhkNDkzYjMzZDI1NTA0NmQzNzk=");
                request.Method = "POST";
                request.ContentLength = data.Length;
                //using (var stream = request.GetRequestStream())
                //{
                //    stream.Write(data, 0, data.Length);
                //}

                var stream = request.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
                WebResponse response = (HttpWebResponse)request.GetResponse();

                Stream responseData = response.GetResponseStream();

                Console.WriteLine(responseData);
                response.Close();
                var responseStream = response.GetResponseStream();

                var mapImage = response.GetResponseStream();
                // data here is optional, in case we recieve any string data back from the POST request.
                var responseString = UnicodeEncoding.UTF8.GetString(data);
                Console.WriteLine(responseString);
            }
            catch (Exception)
            {

                throw;
            }

            try
            {
                NameValueCollection postData = new NameValueCollection()
               {
                      { "Authorization", "Basic YWE0ZjZiZDIyYjc3NGFiMzk2ODI1NzQ5NzNjYjIyNWY6MzhiODYwMDhmOGJkNDhkNDkzYjMzZDI1NTA0NmQzNzk=" },  //order: {"parameter name", "parameter value"}
                      { "Content-type", "application/x-www-form-urlencoded" }
               };
                using (WebClient wc = new WebClient())
                {
                    wc.QueryString.Add("grant_type", "client_credentials");
                    wc.Headers.Add("Authorization", "Basic YWE0ZjZiZDIyYjc3NGFiMzk2ODI1NzQ5NzNjYjIyNWY6MzhiODYwMDhmOGJkNDhkNDkzYjMzZDI1NTA0NmQzNzk=");
                    wc.Headers.Add("Content-type", "application/x-www-form-urlencoded");
                   
                    // client.UploadValues returns page's source as byte array (byte[])
                    // so it must be transformed into a string
                    // var response = UnicodeEncoding.UTF8.GetString(wc.UploadValues(requestUrl, "POST", wc));

                    //JObject deserializedResponse = JObject.Parse(response);

                    //responseObject = deserializedResponse;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Console.Read();
                return null;
            }

            // Retrieve the token and return
            //string accessToken = (string)responseObject["acces_token"];


            //Artist artist = new Artist
            //{
            //    id = artistId,
            //    name = artistName
            //};
        Console.WriteLine();
            return "test";
        }


        public static JObject GetJsonResponse(string requestUrl)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    // TODO dyamically pull bearer token from app.config
                    wc.Headers.Add("Authorization", "Bearer BQAo6EZjqlvaZ-IhQ8teorO6LwcSgEB0GJ5lyjRymPBFdcvLWMKNXm26htnpuogrK-3rF6oywhVjs5o_qP0");
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
