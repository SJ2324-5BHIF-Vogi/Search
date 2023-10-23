using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Spg.Search.Search.Model
{
    public class Content
    {
        public int Id { get; set; }
        public Guid content_guid { get; set; }
        public int fk_content_id { get; set; }
    }
}
