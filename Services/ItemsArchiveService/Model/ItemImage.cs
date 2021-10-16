using MongoDB.Bson.Serialization.Attributes;

namespace ItemsArchiveService.Model
{
    public class ItemImage
    {
        [BsonElement("path")]
        public string Path { get; set; }

        [BsonElement("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}