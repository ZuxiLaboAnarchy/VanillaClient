using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Vanilla.Config
{

    public class ColorConverter : JsonConverter<Color>
    {
        public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
        {
            // Serialize the Color as an object with r, g, b, a fields
            writer.WriteStartObject();
            writer.WritePropertyName("r");
            writer.WriteValue(value.r);
            writer.WritePropertyName("g");
            writer.WriteValue(value.g);
            writer.WritePropertyName("b");
            writer.WriteValue(value.b);
            writer.WritePropertyName("a");
            writer.WriteValue(value.a);
            writer.WriteEndObject();
        }


        public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            // Deserialize the JSON object back to Color
            float r = 0, g = 0, b = 0, a = 1; // Default alpha to 1 for fully opaque

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string propertyName = reader.Value.ToString();
                    reader.Read(); // Move to the value
                    switch (propertyName)
                    {
                        case "r":
                            r = Convert.ToSingle(reader.Value);
                            break;
                        case "g":
                            g = Convert.ToSingle(reader.Value);
                            break;
                        case "b":
                            b = Convert.ToSingle(reader.Value);
                            break;
                        case "a":
                            a = Convert.ToSingle(reader.Value);
                            break;
                    }
                }

                if (reader.TokenType == JsonToken.EndObject)
                {
                    break;
                }
            }

            return new Color(r, g, b, a);
        }
    }
}
