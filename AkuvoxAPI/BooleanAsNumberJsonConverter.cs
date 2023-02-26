using System;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable CA1062

namespace AkuvoxAPI;

public class BooleanAsNumberJsonConverter : JsonConverter<bool>
{
	public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) =>
		writer.WriteNumberValue(Convert.ToInt32(value));

	public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		reader.TokenType switch
		{
			JsonTokenType.String => int.TryParse(reader.GetString(), out var n)
				? Convert.ToBoolean(n)
				: throw new JsonException(),
			JsonTokenType.Number => reader.TryGetInt32(out var n) ? Convert.ToBoolean(n) : throw new JsonException(),
			_ => throw new JsonException(),
		};
}
