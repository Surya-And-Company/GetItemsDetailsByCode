using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ItemsArchiveService.Model
{
    public class ThirdPartyAllowed
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id {get;set;}

        [BsonElement("company")]
        public string Company { get;set;}

        [BsonElement("reg_date")]
        public DateTime RegistrationDate{ get;set;}

        [BsonElement("a_date")]
        public DateTime ActivationDate{ get; set;}

        [BsonElement("e_date")]
        public DateTime ExpiresDate { get;set;}

        [BsonElement("a_request")]
        public long AllowedRequest{ get;set;}

        [BsonElement("r_count")]
        public long RequestCount{ get;set;}

        [BsonElement("token")]
        public string Token{ get;set;}
    }
}