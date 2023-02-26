using System.Text.Json.Serialization;

namespace AkuvoxAPI.Models;

public record Relay
{
	[JsonPropertyName("relay_id")]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
	public int RelayID { get; set; }

	[JsonPropertyName("dtmf")]
	public string Dtmf { get; set; } = null!;

	[JsonPropertyName("door_name")]
	public string DoorName { get; set; } = null!;

	[JsonPropertyName("show_home")]
	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	public bool ShowHome { get; set; }

	[JsonPropertyName("show_talking")]
	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	public bool ShowTalking { get; set; }

	[JsonPropertyName("enable")]
	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	public bool Enabled { get; set; }

	[JsonPropertyName("relay_status")]
	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	public bool IsRelayActivated { get; set; }
}
