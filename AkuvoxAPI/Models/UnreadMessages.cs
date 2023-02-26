using System.Text.Json.Serialization;

namespace AkuvoxAPI.Models;

public record UnreadMessages
{
	[JsonPropertyName("messages_num")]
	public int Messages { get; set; }

	[JsonPropertyName("activities_num")]
	public int Activities { get; set; }
}
