using MongoDB.Bson.Serialization.Attributes;

namespace Database
{
    public class AccountDocument
    {
        [BsonId]
        public int AccountId { get; set; }
        public int Balance { get; set; }
    }
}
