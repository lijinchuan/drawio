﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawio.Net.Domain.Entity
{
    public class MongoDrawFileDelEntity
    {
        public const string DBName = "DrawIO";
        public const string CollectionName = "DrawFileDel";

        public ObjectId _id
        {
            get;
            set;
        }

        public long Fid
        {
            get;
            set;
        }

        public string UserId
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }

        public bool IsValid
        {
            get;
            set;
        }

        public DateTime CrateTime
        {
            get;
            set;
        }

        public DateTime UpdateTime
        {
            get;
            set;
        }

        public DateTime DelTime
        {
            get;
            set;
        }
    }
}
