using System;

namespace AkuvoxAPI;

public class AkuvoxException : Exception
{
	public int? Status { get; }

	public AkuvoxException()
	{
	}

	public AkuvoxException(string? message) : base(message)
	{
	}

	public AkuvoxException(string? message, Exception innerException) : base(message, innerException)
	{
	}

	public AkuvoxException(int status, string? errorMessage) : base($"Failed with status {status}: \"{errorMessage}\"")
	{
		Status = status;
	}
}
