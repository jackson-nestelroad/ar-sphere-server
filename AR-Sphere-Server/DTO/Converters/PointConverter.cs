using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ARSphere.DTO.Converters
{
    /// <summary>
    /// <para>JSON Converter for the Point data type in NetTopologySuite.</para>
    /// <para>Only uses x and y data fields.</para>
    /// </summary>
    public class PointConverter : JsonConverter<Point>
    {
        public override Point Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            JsonElement parsed = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
            return new Point(parsed.GetProperty("X").GetDouble(), parsed.GetProperty("Y").GetDouble()) 
            {
                SRID = 4326
            };
        }

        public override void Write(Utf8JsonWriter writer, Point value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("X", value.X);
            writer.WriteNumber("Y", value.Y);
            writer.WriteEndObject();
        }
    }
}
