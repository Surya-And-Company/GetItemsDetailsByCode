using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ItemsArchiveService.Model
{
    public class Log
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string LogId{get;set;}

        [BsonElement("msg")]
        public string ExceptionMsg{get;set;}

        [BsonElement("type")]
        public string ExceptionType { get; set; }

        [BsonElement("url")]
        public string ExceptionURL { get; set; }       

        [BsonElement("stacktrace")]
        public string StackTrace { get; set; }

        [BsonElement("date")]
        public DateTime LogDate  { get; set; }

        [BsonElement("isresolved")]
        public bool IsResolved  { get; set; }
    }
}