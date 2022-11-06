using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Calculator
{
    public class Producs
    {
        [BsonElement("Trade_Item_Description")]
        public string Trade_Item_Description { get; set; }
        public string Fats { get; set; }
        public string Carbohydrates { get; set; }
        public string Proteins { get; set; }
        public string DietaryFiber { get; set; }
        public string SaturatedFattyAcids { get; set; }
        public string Dessert { get; set; }

        public ObjectId _id { get; set; }
    }
}
