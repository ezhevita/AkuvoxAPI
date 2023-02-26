using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AkuvoxAPI.Models;

public record Device
{
	[JsonPropertyName("mac")]
	public string MacAddress { get; set; } = null!;

	[JsonPropertyName("status")]
	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	public bool Status { get; set; }

	[JsonPropertyName("rtsp_nonce")]
	public string RtspNonce { get; set; } = null!;

	[JsonPropertyName("sip")]
	public string SipNumber { get; set; } = null!;

	[JsonPropertyName("location")]
	public string Location { get; set; } = null!;

	[JsonPropertyName("rtsp_pwd")]
	public string RtspPassword { get; set; } = null!;

	[JsonPropertyName("dev_type")]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
	public int DeviceType { get; set; }

	[JsonPropertyName("is_public")]
	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	public bool IsPublic { get; set; }

	[JsonPropertyName("dclient_ver")]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
	public int DeviceClientVersion { get; set; }

	[JsonPropertyName("dtmf")]
	public string Dtmf { get; set; } = null!;

	[JsonPropertyName("firmware")]
	public string Firmware { get; set; } = null!;

	[JsonPropertyName("is_need_monitor")]
	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	public bool NeedsMonitor { get; set; }

	[JsonPropertyName("arming_function")]
	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	public bool HasArmingFunction { get; set; }

	[JsonPropertyName("relay")]
	public IReadOnlyCollection<Relay> Relays { get; set; } = null!;
}
