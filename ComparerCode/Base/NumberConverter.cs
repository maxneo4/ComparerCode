using Newtonsoft.Json;
using System;

namespace ComparerCode.Base
{
    public class NumberConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(double) || objectType == typeof(long) || objectType == typeof(int);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            double dvalue = double.Parse(value.ToString());
            serializer.Serialize(writer, dvalue.ToString("#,##0").Replace(".", " "));
        }
    }
}