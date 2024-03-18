using System.Text.Json.Serialization;
using System.Text.Json;

namespace HRDomain.CustomConverter
{
    public class TimeCustomConvertor: JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            string timeString = reader.GetString();
            if (!TimeSpan.TryParse(timeString, out TimeSpan time))
            {
                throw new JsonException($"Invalid TimeSpan format: {timeString}");
            }

            return time;
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}

