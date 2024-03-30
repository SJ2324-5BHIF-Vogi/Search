using Spg.ContentTeil.DomainModel.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.ContentTeil.Repository
{
    public interface IExternalDatabaseRepository
    {
        Task<List<ContentResultDto>> ContentResultsAsync(string query, string filter, string sort);
    }
}
