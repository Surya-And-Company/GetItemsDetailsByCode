using System.Collections.Generic;
using ItemsArchiveService.Model;
using MongoDB.Bson.Serialization.Attributes;

namespace ItemsArchiveService.DTO
{
    public class GetItemByCodeResponseDTO
    {
            
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
        public List<ItemImage> ItemImage { get; set; }      
    }
}