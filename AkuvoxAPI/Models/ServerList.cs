using System.Text.Json.Serialization;

namespace AkuvoxAPI.Models;

public record ServerList : RestServerList
{
	[JsonPropertyName("access_server")]
	public string AccessServer { get; set; } = null!;

	[JsonPropertyName("access_server_ipv6")]
	public string AccessServerIPv6 { get; set; } = null!;

	[JsonPropertyName("pbx_server")]
	public string PbxServer { get; set; } = null!;

	[JsonPropertyName("pbx_server_ipv6")]
	public string PbxServerIPv6 { get; set; } = null!;

	[JsonPropertyName("platform_ver")]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
	public int PlatformVersion { get; set; }

	[JsonPropertyName("rtmp_server")]
	public string RtmpServer { get; set; } = null!;

	[JsonPropertyName("rtmp_server_ipv6")]
	public string RtmpServerIPv6 { get; set; } = null!;

	[JsonPropertyName("smarthome_site")]
	public string SmartHomeSite { get; set; } = null!;

	[JsonPropertyName("smarthome_uid")]
	public string SmartHomeUid { get; set; } = null!;

	[JsonPropertyName("token")]
	public string Token { get; set; } = null!;

	[JsonPropertyName("vrtsp_server")]
	public string VrtspServer { get; set; } = null!;

	[JsonPropertyName("vrtsp_server")]
	public string VrtspServerIPv6 { get; set; } = null!;

	[JsonPropertyName("web_server")]
	public string WebServer { get; set; } = null!;

	[JsonPropertyName("web_server")]
	public string WebServerIPv6 { get; set; } = null!;
}
