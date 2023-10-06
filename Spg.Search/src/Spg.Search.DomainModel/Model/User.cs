using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Search.DomainModel.Model
{
    internal class User
    {
        public int Id { get; set; }
        public int fk_user_id { get; set; }
    }
}
