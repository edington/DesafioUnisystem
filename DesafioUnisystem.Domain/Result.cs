namespace DesafioUnisystem.Domain;

public class Result<T>
{
    public bool Success { get; private set; }
    public string ErrorMessage { get; private set; }
    public T Value { get; private set; }

    private Result(bool success, T value, string errorMessage)
    {
        Success = success;
        Value = value;
        ErrorMessage = errorMessage;
    }

    public static Result<T> Ok(T value)
    {
        return new Result<T>(true, value, null);
    }

    public static Result<T> Fail(string errorMessage)
    {
        return new Result<T>(false, default(T), errorMessage);
    }
}

