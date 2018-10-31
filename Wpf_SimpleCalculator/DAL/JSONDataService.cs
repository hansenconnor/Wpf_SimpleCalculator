using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_SimpleCalculator.Models;

namespace Wpf_SimpleCalculator.DAL
{
    public class JSONDataService : IDataService
    {
        // Return dynamic search object
        public string getDynamicQueryResult( string searchQuery, ItemType type, int limit )
        {
            string requestUrl = string.Format("https://");

            string requestUrl = string.Format("http://spatial.virtualearth.net/REST/v1/data/{0}/{1}/{2}" +
        "?spatialFilter=nearby({3},{4},{5})&key={6}", accessId, dataSourceName,
        dataEntityName, SearchLatitude, SearchLongitude, Radius, bingMapsKey);
            // Send the request and get back an XML response.
            XmlDocument response = GetXmlResponse(requestUrl);
            // Display each entity's info.
            ProcessEntityElements(response);
        }


        public static XmlDocument GetXmlResponse(string requestUrl)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(response.GetResponseStream());
                return (xmlDoc);

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
