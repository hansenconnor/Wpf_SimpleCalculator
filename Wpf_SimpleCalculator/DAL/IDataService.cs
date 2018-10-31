using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf_SimpleCalculator.Models;

namespace Wpf_SimpleCalculator.DAL
{
    public interface IDataService
    {
        dynamic getDynamicQueryResult(string searchQuery, ItemType type, int limit);
    }
}
