using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Search.DomainModel.Model
{
    public class SearchHistory
    {
        public int Id { get; init; }
        public Guid guid { get; init; } = Guid.NewGuid();
        public string searchUsername  { get; set; }
        public string searchContent { get; set; }
        public SearchHistory()
        {
        }

        public SearchHistory(int id, Guid guid, string searchUsername, string searchContent)
        {
            if (id < 0)
            {
                throw new ArgumentException("ID cannot be negative", nameof(id));
            }

            Id = id;
            this.guid = guid;
            this.searchUsername = searchUsername;
            this.searchContent = searchContent;
        }
    }
}
