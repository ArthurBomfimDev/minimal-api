﻿using Minimal.Domain.Enuns.Notification;

namespace Minimal.Application.ModelViews.ApiResponse;

public class DetailedNotification(string? identifier, List<string>? listMessage, EnumNotificationType notificationType)
{
    public string? Identifier { get; set; } = identifier;
    public List<string>? ListMessage { get; set; } = listMessage;
    public EnumNotificationType NotificationType { get; set; } = notificationType;
}