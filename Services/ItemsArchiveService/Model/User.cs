using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ItemsArchiveService.Model
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("mobile")]
        public string MobileNo { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("salt")]
        public string Salt { get; set; }

        [BsonElement("p_image")]
        public string ProfileImage { get; set; }

        [BsonElement("Role")]
        public List<string> Roles { get; set; }

        [BsonElement("isdeleted")]
        public bool IsDeleted { get; set; }

        [BsonElement("IsActive")]
        public bool IsActive { get; set; }

        [BsonElement("c_date")]
        public DateTime CreatedDate { get; set; }        

        [BsonElement("l_u_date")]
        public DateTime LastUpdatedDate { get; set; }
    }
}