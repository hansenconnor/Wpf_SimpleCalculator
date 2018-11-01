using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_SimpleCalculator.DAL;
using Wpf_SimpleCalculator.Models;

namespace Wpf_SimpleCalculator.BLL
{
    class ArtistsBLL
    {
        IDataService _dataService;


        public ArtistsBLL(IDataService dataService)
        {
            _dataService = dataService;
        }


        public List<Artist> getArtists( string searchQuery, ItemType type, int limit )
        {
            // Instantiate list to hold artists
            List<Artist> artists = new List<Artist>();

            // Get the artists from the query
            JObject responseObject = _dataService.getDynamicQueryResult(searchQuery, type, limit);

            string artistName = (string)responseObject["artists"]["items"][0]["name"];
            string artistId = (string)responseObject["artists"]["items"][0]["id"];


            Artist artist = new Artist
            {
                id = artistId,
                name = artistName
            };

            // TODO: Foreach artist in response (5 max) create new artist and push to array, then display as selectable dropdown under search.

            Console.WriteLine();
            //JArray categories = (JArray)responseObject["artists"]["items"][0]["name"];
            // ["Json.NET", "CodePlex"]

            //IList<string> categoriesText = categories.Select(c => (string)c).ToList();






            // Iterate through the response object and push items to artist list
            //foreach (var artist in responseObject)
            //{
            //    var id = responseObject["report"]["Id"].ToString();
            //    int id = artist.Key
            //    artists.Add(new Artist());
            //}
            
            // Return list of artists
            return artists;
        }
    }
}
