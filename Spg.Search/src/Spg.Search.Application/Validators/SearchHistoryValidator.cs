using Spg.Search.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Search.Application.Validators
{
    public class SearchHistoryValidator
    {
        public bool Validate(SearchHistory searchHistory)
        {
            if (searchHistory == null)
            {
                Console.WriteLine("SearchHistory is null.");
                return false;
            }

            if (searchHistory.searchUsername == null)
            {
                Console.WriteLine("SearchHistory.searchUsername is null.");
                return false;
            }

            if (searchHistory.searchContent == null)
            {
                Console.WriteLine("SearchHistory.searchContent is null.");
                return false;
            }

            return true;
        }
    }
}
