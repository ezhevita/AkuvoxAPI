using System.Text.Json.Serialization;

namespace AkuvoxAPI.Models;

public record GateServerList
{
	[JsonPropertyName("gate_serverV4")]
	public string GateServerIPv4 { get; set; } = null!;

	[JsonPropertyName("gate_serverV6")]
	public string GateServerIPv6 { get; set; } = null!;
}
