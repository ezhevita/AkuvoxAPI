using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AkuvoxAPI.Models;

public record AppConfiguration
{
	[JsonPropertyName("surplus_expire_day")]
	public int SurplusExpireDay { get; set; }

	[JsonPropertyName("account_expire")]
	public long AccountExpire { get; set; }

	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	[JsonPropertyName("show_payment")]
	public bool ShowPayment { get; set; }

	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	[JsonPropertyName("show_subscription")]
	public bool ShowSubscription { get; set; }

	[JsonPropertyName("sip")]
	public string SipNumber { get; set; } = null!;

	[JsonPropertyName("display_name")]
	public string DisplayName { get; set; } = null!;

	[JsonPropertyName("sip_server")]
	public string SipServer { get; set; } = null!;

	[JsonPropertyName("sip_server_ipv6")]
	public string SipServerIPv6 { get; set; } = null!;

	[JsonPropertyName("sip_passwd")]
	public string SipPasswd { get; set; } = null!;

	[JsonPropertyName("motion_alert")]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
	public int MotionAlert { get; set; }

	[JsonPropertyName("video_res")]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
	public int VideoRes { get; set; }

	[JsonPropertyName("video_bitrate")]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
	public int VideoBitrate { get; set; }

	[JsonPropertyName("video_storage_time")]
	public object? VideoStorageTime { get; set; }

	[JsonPropertyName("have_public_dev")]
	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	public bool HavePublicDev { get; set; }

	[JsonPropertyName("show_landline")]
	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	public bool ShowLandline { get; set; }

	[JsonPropertyName("enable_pin_config")]
	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	public bool EnablePinConfig { get; set; }

	[JsonPropertyName("uid")]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
	public long Uid { get; set; }

	[JsonPropertyName("trans_type")]
	[JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
	public int TransType { get; set; }

	[JsonPropertyName("codec")]
	public string Codec { get; set; } = null!;

	[JsonPropertyName("data_collection")]
	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	public bool DataCollection { get; set; }

	[JsonPropertyName("landline")]
	public IReadOnlyCollection<string> Landline { get; set; } = null!;

	[JsonPropertyName("rtp_confuse")]
	public long RtpConfuse { get; set; }

	[JsonPropertyName("show_tempkey")]
	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	public bool ShowTempkey { get; set; }

	[JsonPropertyName("role")]
	public int Role { get; set; }

	[JsonPropertyName("check_dev")]
	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	public bool CheckDev { get; set; }

	[JsonPropertyName("check_slave")]
	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	public bool CheckSlave { get; set; }

	[JsonPropertyName("show_thirdparty_lock")]
	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	public bool ShowThirdpartyLock { get; set; }

	[JsonPropertyName("show_face")]
	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	public bool ShowFace { get; set; }

	[JsonPropertyName("enable_confirm_flag")]
	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	public bool EnableConfirmFlag { get; set; }

	[JsonPropertyName("enable_third_camera")]
	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	public bool EnableThirdCamera { get; set; }

	[JsonPropertyName("community_contact")]
	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	public bool CommunityContact { get; set; }

	[JsonPropertyName("pin_init")]
	[JsonConverter(typeof(BooleanAsNumberJsonConverter))]
	public bool PinInit { get; set; }
}
