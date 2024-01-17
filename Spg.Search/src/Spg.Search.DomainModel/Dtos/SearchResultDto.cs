using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Search.DomainModel.Dtos
{
    public class SearchResultDto
    {
        public int Id { get; set; }
        public string searchUsername { get; set; }
        public string searchContent { get; set; }
    }
}
