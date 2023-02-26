using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AkuvoxAPI.Models;
using Flurl.Http;
using Flurl.Serialization.TextJson;

namespace AkuvoxAPI;

[SuppressMessage("Design", "CA1001:Types that own disposable fields should be disposable")]
public sealed class AkuvoxGateClient : IAkuvoxGateClient
{
	private const string directoryServerURL = "http://rps.akuvox.com:8080/";

	private AkuvoxGateClient(EAkuvoxServer server)
	{
		_server = server;
	}

	private readonly EAkuvoxServer _server;
	private IFlurlClient _httpClient = null!;
	private string? _restServer;
	private string? _token;
	private bool _isAuthenticated;

	public async Task AuthenticateUsingToken(string token)
	{
		_token = token;

		var servers = await GetRestServerList();
		_restServer = servers.RestServerHttps;

		var response = await _httpClient.Request("https://" + _restServer + "/", "login_conf")
			.WithHeader("api-version", "6.5")
			.SetQueryParam("token", _token)
			.GetJsonAsync<AkuvoxResponse<object>>();

		response.ValidateResponse();

		_isAuthenticated = true;
	}

	public static async Task<AkuvoxGateClient> CreateAsync(EAkuvoxServer server)
	{
		var client = new AkuvoxGateClient(server);
		await client.Initialize();

		return client;
	}

	private async Task Initialize()
	{
		using var directoryClient = new FlurlClient(directoryServerURL);
		directoryClient.Configure(settings => settings.WithTextJsonSerializer());

		var gatesResponse = await directoryClient.Request("app", "redirect")
			.SetQueryParam("mac", ((int) _server).ToString("D12", CultureInfo.InvariantCulture))
			.GetJsonAsync<AkuvoxResponse<GateServerList>>();

		gatesResponse.ValidateResponse();

		_httpClient = new FlurlClient("http://" + gatesResponse.Data.GateServerIPv4 + "/");

		_httpClient.Configure(settings => settings.WithTextJsonSerializer());
	}

	public async Task<ServerList> Authenticate(string login, string password)
	{
		const byte CaesarKey = 3;

		var cipherLogin = Utilities.CaesarEncrypt(login, CaesarKey);

		var passwordHash = Utilities.MD5Hash(Utilities.MD5Hash(password));

		await using var response = await _httpClient.Request("login")
			.SetQueryParam("user", cipherLogin, true)
			.SetQueryParam("passwd", passwordHash)
			.WithHeader("api-version", "6.4")
			.GetStreamAsync();

		if (response == null)
			throw new AkuvoxException("Failed sending the request");

		await using var decryptedStream = DecryptStream(login, response);

		var loginResponse = await JsonSerializer.DeserializeAsync<AkuvoxResponse<ServerList>>(decryptedStream);
		loginResponse.ValidateResponse();

		_isAuthenticated = true;
		_token = loginResponse.Data.Token;
		_restServer = loginResponse.Data.RestServerHttps;

		return loginResponse.Data;
	}

	private static Stream DecryptStream(string login, Stream inputStream)
	{
		var encryptionKeySuffix = "Akuvox55069013!@"u8;

		var encryptionKey = MD5.HashData(Encoding.UTF8.GetBytes(login));
		encryptionKeySuffix.CopyTo(encryptionKey.AsSpan()[16..]);

		var key = encryptionKey;
		var iv = "1234567887654321"u8.ToArray();

		using var encryption = Aes.Create();
		encryption.Mode = CipherMode.CBC;

		var aesDecryptor = encryption.CreateDecryptor(key, iv);

#pragma warning disable CA2000
		var base64Stream = new CryptoStream(inputStream, new FromBase64Transform(), CryptoStreamMode.Read);
#pragma warning restore CA2000
		var aesStream = new CryptoStream(base64Stream, aesDecryptor, CryptoStreamMode.Read);

		return aesStream;
	}

	private static string Encrypt(string login, string input)
	{
		var encryptionKeySuffix = "Akuvox55069013!@"u8;

		var encryptionKey = MD5.HashData(Encoding.UTF8.GetBytes(login));
		encryptionKeySuffix.CopyTo(encryptionKey.AsSpan()[16..]);

		var key = encryptionKey;
		var iv = "1234567887654321"u8.ToArray();

		using var encryption = Aes.Create();
		encryption.Mode = CipherMode.CBC;
		encryption.Key = key;

		var encrypted = encryption.EncryptCbc(Encoding.UTF8.GetBytes(input), iv);

		return Convert.ToBase64String(encrypted);
	}

	public async Task<RestServerList> GetRestServerList()
	{
		var response = await _httpClient.Request("rest_server")
			.WithHeader("api-version", "6.0")
			.GetJsonAsync<AkuvoxResponse<RestServerList>>();

		response.ValidateResponse();

		return response.Data;
	}

	public AkuvoxClient GetAkuvoxClient()
	{
		if (!_isAuthenticated)
			throw new AkuvoxException("User is not authenticated");

		return new AkuvoxClient("https://" + _restServer + "/", _token!);
	}

	public async Task<ServerList> GetServerList(string login, string password)
	{
		if (!_isAuthenticated)
			throw new AkuvoxException("User is not authenticated");

		var innerRequest = new ServerListRequest
		{
			Password = Utilities.MD5Hash(Utilities.MD5Hash(password)),
			Token = _token!
		};

		var encryptedRequest = Encrypt(login, JsonSerializer.Serialize(innerRequest));

		var request = new AkuvoxEncryptedRequest
		{
			Data = encryptedRequest,
			User = login
		};

		var response = await _httpClient.Request("servers_list")
			.WithHeader("api-version", "6.4")
			.PostJsonAsync(request)
			.ReceiveJson<AkuvoxBaseResponse<string>>();

		if (response == null)
			throw new AkuvoxException("Failed sending the request");

		using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(response.Data));

		await using var decryptedStream = DecryptStream(login, memoryStream);
		var serverListResponse = await JsonSerializer.DeserializeAsync<AkuvoxResponse<ServerList>>(decryptedStream);
		serverListResponse.ValidateResponse();

		return serverListResponse.Data;
	}
}
