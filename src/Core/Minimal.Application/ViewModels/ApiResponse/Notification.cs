using Minimal.Domain.Enuns.Notification;

namespace Minimal.Application.ModelViews.ApiResponse;

public class Notification
{
    public string Message { get; set; }
    public EnumNotificationType Type { get; set; }

    public Notification(string message, EnumNotificationType type)
    {
        Message = message;
        Type = type;
    }
}