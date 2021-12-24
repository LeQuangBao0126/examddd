using Examination.Domain.SeedWork;
using MongoDB.Bson.Serialization.Attributes;

namespace Examination.Domain.AggregateModels.CategoryAggregate
{
    public class Category : Entity , IAggregateRoot
    {
        [BsonElement("name")]
        public string Name { get; set; }
        
        [BsonElement("urlPath")]
        public string UrlPath { get; set; }  // urlPath : domain/exam-category/ / 
    }
}