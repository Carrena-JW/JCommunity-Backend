namespace JCommunity.AppCore.Core.Models;

public record Error<T>(T data, string message, int statusCode);
