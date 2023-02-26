using AkuvoxAPI;

#pragma warning disable CA1852

var gateClient = await AkuvoxGateClient.CreateAsync(EAkuvoxServer.EMEA);
if (File.Exists("token.txt"))
{
	var token = await File.ReadAllTextAsync("token.txt");
	await gateClient.AuthenticateUsingToken(token);
} else
{
	var credentials = (await File.ReadAllTextAsync("creds.txt")).Split(':');
	var response = await gateClient.Authenticate(credentials[0], credentials[1]);

	await File.WriteAllTextAsync("token.txt", response.Token);
}

var client = gateClient.GetAkuvoxClient();

var config = await client.GetUserConfiguration();

var firstDevice = config.DevicesList.First();

var macAddress = firstDevice.MacAddress;
var relayId = firstDevice.Relays.First().RelayID;

while (true)
{
	Console.WriteLine("Press Enter to open the door");
	Console.ReadLine();

	await client.OpenDoor(relayId, macAddress);
}
