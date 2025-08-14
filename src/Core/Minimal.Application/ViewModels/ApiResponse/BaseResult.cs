namespace Minimal.Application.ModelViews.ApiResponse;

public class BaseResult<T>(bool isSuccess, List<Notification> listNotification, T? value)
{
    public bool IsSuccess { get; private set; } = isSuccess;
    public List<Notification> listNotification { get; private set; } = listNotification;
    public T? Value { get; private set; } = value;

    public static BaseResult<T> Success(T value) => new(true, [], value);
    public static BaseResult<T> Failure() => new(false, [], default);
    public static BaseResult<T> Success(T value, List<Notification> ListNotification) => new(true, ListNotification, value);
    public static BaseResult<T> Failure(List<Notification> listNotification) => new(false, listNotification, default);
    public static BaseResult<T> Success(T value, Notification notification) => new(true, [notification], value);
    public static BaseResult<T> Failure(Notification notification) => new(false, [notification], default);
}