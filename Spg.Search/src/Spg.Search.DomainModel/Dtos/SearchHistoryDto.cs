using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Search.DomainModel.Dtos
{
    public record SearchHistoryDto
    {
        //SearchHistoryDto Properties
        public int Id { get; set; }
        public Guid guid { get; set; } = Guid.NewGuid();
        public string searchUsername { get; set; }
        public string searchContent { get; set;}
    }
}
