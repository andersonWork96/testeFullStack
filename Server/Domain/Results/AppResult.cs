namespace Server.Domain.Results;

public enum AppResultStatus
{
    Success,
    NotFound,
    Invalid
}

public record AppResult(AppResultStatus Status, string? Error = null)
{
    public bool IsSuccess => Status == AppResultStatus.Success;
    public bool IsNotFound => Status == AppResultStatus.NotFound;
    public bool IsInvalid => Status == AppResultStatus.Invalid;

    public static AppResult Success() => new(AppResultStatus.Success);
    public static AppResult NotFound() => new(AppResultStatus.NotFound);
    public static AppResult Invalid(string error) => new(AppResultStatus.Invalid, error);
}

public record AppResult<T>(T? Value, AppResultStatus Status, string? Error = null)
{
    public bool IsSuccess => Status == AppResultStatus.Success;
    public bool IsNotFound => Status == AppResultStatus.NotFound;
    public bool IsInvalid => Status == AppResultStatus.Invalid;

    public static AppResult<T> Success(T value) => new(value, AppResultStatus.Success);
    public static AppResult<T> NotFound() => new(default, AppResultStatus.NotFound);
    public static AppResult<T> Invalid(string error) => new(default, AppResultStatus.Invalid, error);
}
