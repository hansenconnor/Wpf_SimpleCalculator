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

        string searchQuery;
        ItemType type;
        int limit;


        public ArtistsBLL(IDataService dataService)
        {
            _dataService = dataService;
        }


        public List<Artist> getArtists()
        {
            // Instantiate list to hold artists
            List<Artist> artists = new List<Artist>();

            // Get the artists from the query
            artists = _dataService.getDynamicQueryResult(searchQuery, type, limit);
            
            // Return list of artists
            return artists;
        }
    }
}
