using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Domain.Entity
{
    public class MongoAutoId
    {
        public const string DBName = "DrawIO";
        public const string CollectionName = "MongoAutoId";

        public ObjectId _id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public long LastId
        {
            get;
            set;
        }
    }
}
