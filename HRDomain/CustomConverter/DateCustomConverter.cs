using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace HRDomain.CustomConverter
{
    public class DateCustomConverter: JsonConverter<DateOnly>
    {
        private const string DateFormat = "MM/dd/yyyy";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            string dateString = reader.GetString();
            if (!DateOnly.TryParseExact(dateString, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly date))
            {
                throw new JsonException($"Invalid date format: {dateString}");
            }

            return date;
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(DateFormat));
        }

    }
}




