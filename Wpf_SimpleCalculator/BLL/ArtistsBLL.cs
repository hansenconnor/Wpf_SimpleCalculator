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

        /// <summary>
        /// Return a list of artists from user search query
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <param name="type"></param>
        /// <param name="limit"></param>
        /// <returns>List of artists</returns>
        public List<Artist> GetArtists( string searchQuery, ItemType type, int limit )

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

            artists.Add(artist);

            return artists;

            // TODO: Foreach artist in response (5 max) create new artist and push to array, then display as selectable dropdown under search.
        }
    }
}
