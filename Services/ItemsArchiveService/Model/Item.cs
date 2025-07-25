using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ItemsArchiveService.Model
{
    public class Item
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id {get;set;}

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("u_id")]
        public string UserId {get;set;}
        
        [BsonElement("code")]
        public string Code { get; set; }

        [BsonElement("brand")]
        public string Brand { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("s_price")]
        public decimal SellingPrice { get; set; }

        [BsonElement("desc")]
        public string Description { get; set; }

        [BsonElement("img")]
        public List<ItemImage> ItemImages { get; set; }

        [BsonElement("isapproved")]
        public bool IsApproved { get; set; }

        [BsonElement("c_date")]
        public DateTime CreatedDate { get; set; }

    }
}