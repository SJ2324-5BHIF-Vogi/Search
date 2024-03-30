using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.ContentTeil.DomainModel.Dtos
{
    public record ContentDto
    {
        //SearchHistoryDto Properties
        public int Id { get; set; }
        public Guid guid { get; set; } = Guid.NewGuid();
        public string text { get; set; }
    }
}
