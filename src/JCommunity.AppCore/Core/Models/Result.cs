namespace JCommunity.AppCore.Core.Models;

public record Result<T>(T data, bool isError, Error<T> error)where T : notnull
{
    public static Result<T> Create(T data, bool isError = false, Error<T> error = default!)
    {
        return ((data == null || isError == true) ? new(data, isError, error) : new(data, isError, error));
    }
}
