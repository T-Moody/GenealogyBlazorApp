namespace GenealogyBlazorApp.Core.Shared;

public class Result
{
    public bool IsSuccess { get; protected set; }
    public bool IsFailure => !IsSuccess;
    public string? Error { get; protected set; }

    public static Result Success() => new() { IsSuccess = true };
    public static Result Failure(string error) => new() { IsSuccess = false, Error = error };
    
    public static Result<T> Success<T>(T value) => new(value);
    public static Result<T> Failure<T>(string error) => new(error);
}

public class Result<T> : Result
{
    private readonly T? _value;

    protected internal Result(T value)
    {
        _value = value;
        IsSuccess = true;
    }

    protected internal Result(string error)
    {
        IsSuccess = false;
        Error = error;
    }

    public T Value => IsSuccess ? _value! : throw new InvalidOperationException("Result was a failure, no value exists.");
}