using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Calculator
{
    public class Producs
    {
        [BsonElement("Trade_Item_Description")]
        public string Trade_Item_Description { get; set; }
        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public float Fats { get; set; }
        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public float Carbohydrates { get; set; }
        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public float Proteins { get; set; }
        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public float DietaryFiber { get; set; }
        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public float SaturatedFattyAcids { get; set; }
        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public float Dessert { get; set; }

        public ObjectId _id { get; set; }
    }
}
