namespace Urbamais.Application.Shared;
public sealed class Result
{
    public bool IsSuccess { get; }
    public bool IsNotFound => Code == "not_found";
    public string Code { get; }           // "ok", "not_found", "validation", "conflict", etc.
    public string[] Errors { get; }

    private Result(bool success, string code, string[] errors)
    {
        IsSuccess = success;
        Code = code;
        Errors = errors;
    }

    public static Result Ok() => new(true, "ok", []);
    public static Result NotFound(params string[] errors) => new(false, "not_found", errors ?? []);
    public static Result Fail(params string[] errors) => new(false, "validation", errors ?? []);
    public static Result Conflict(params string[] errors) => new(false, "conflict", errors ?? []);
}

public sealed class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsNotFound => Code == "not_found";
    public string Code { get; }
    public string[] Errors { get; }
    public T? Value { get; }

    private Result(bool success, string code, T? value, string[] errors)
    {
        IsSuccess = success;
        Code = code;
        Value = value;
        Errors = errors;
    }

    public static Result<T> Ok(T value) => new(true, "ok", value, []);
    public static Result<T> NotFound(params string[] errors) => new(false, "not_found", default, errors ?? []);
    public static Result<T> Fail(params string[] errors) => new(false, "validation", default, errors ?? []);
    public static Result<T> Conflict(params string[] errors) => new(false, "conflict", default, errors ?? []);
}
