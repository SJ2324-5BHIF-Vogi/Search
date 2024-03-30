using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Spg.Search.Search.Models
{
    public class User
    {
        public int Id { get; set; }
        public Guid user_guid { get; set; }
        public int fk_user_id { get; set; }
    }
}
