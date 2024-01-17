using Spg.Search.DomainModel.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Search.Repository
{
    public interface IExternalDatabaseRepository
    {
        Task<List<SearchResultDto>> SearchResultsAsync(string query, string filter, string sort);
    }
}
