using Minimal.Domain.Enuns.Notification;

namespace Minimal.Domain.Validation.Result;

public class Notification
{
    public string Message { get; set; }
    public EnumNotificationType Type { get; set; }

    public Notification(string message, EnumNotificationType type)
    {
        Message = message;
        Type = type;
    }

    public static Notification Success(string message) => new Notification(message, EnumNotificationType.Success);
    public static Notification Error(string message) => new Notification(message, EnumNotificationType.Error);
}