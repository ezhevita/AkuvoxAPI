using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using AkuvoxAPI.Models;

namespace AkuvoxAPI;

public static class Utilities
{
	public static void ValidateResponse<T>([NotNull] this AkuvoxResponse<T>? response)
	{
		if (response == null)
			throw new AkuvoxException("Failed sending the request");

		if (!response.Success)
			throw new AkuvoxException(response.Result, response.Message);
	}

#pragma warning disable CA1308
	public static string ToHex(byte[] array) => BitConverter.ToString(array).Replace("-", "", StringComparison.Ordinal)
		.ToLowerInvariant();
#pragma warning restore CA1308

	public static string MD5Hash(string input) => ToHex(MD5.HashData(Encoding.UTF8.GetBytes(input)));

	public static string CaesarEncrypt(string input, byte key)
	{
		const byte AlphabetLength = 26;
		var result = new StringBuilder();

		foreach (var transformedChar in input.Select(
			         chr => (char) (chr switch
			         {
				         >= 'a' and <= 'z' => (chr - 'a' + key) % AlphabetLength + 'a',
				         >= 'A' and <= 'Z' => (chr - 'A' + key) % AlphabetLength + 'A',
				         >= '0' and <= '9' => (chr - '0' + key) % 10 + '0',
				         _ => chr
			         })
		         ))
		{
			result.Append(transformedChar);
		}

		return result.ToString();
	}
}
