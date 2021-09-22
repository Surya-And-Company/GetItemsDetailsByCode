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
        public string Id {get;set;}

        [BsonElement("mobile")]
        public string MobileNo {get;set;}

        [BsonElement("name")]
        public string Name {get;set;}

        [BsonElement("p_image")]
        public string ProfileImage {get;set;}

        [BsonElement("Role")]
        public List<string> Roles {get;set;}

        [BsonElement("isdeleted")]
        public bool IsDeleted {get;set;}

        [BsonElement("IsActive")]
        public bool IsActive {get;set;}
    }
}