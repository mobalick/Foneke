using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GridFS;

namespace EMM.FoNeke.Common.Entities
{
    public class Attachement : BaseEntity
    {
        public string ContentType { get; set; }
        public string Extention { get; set; }
        public string DocumentId { get; set; }

        [BsonIgnore]
        public MongoGridFSFileInfo File { get; set; }
        public ObjectId FileId { get; set; }

        [BsonIgnore]
        public Stream Stream { get; set; }
    }
}
