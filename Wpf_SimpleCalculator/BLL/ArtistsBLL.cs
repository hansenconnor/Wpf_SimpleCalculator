using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_SimpleCalculator.DAL;

namespace Wpf_SimpleCalculator.BLL
{
    class ArtistsBLL
    {
        IDataService _dataService;
        
        public ArtistsBLL(IDataService dataService)
        {
            _dataService = dataService;
        }



        // artistBLL.getArtists(){}


    }
}
