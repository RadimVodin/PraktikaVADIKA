using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

[TaskInfo(29, 2)]
public sealed class Task29_FactoryNotification : ITask
{
    public string Title => "Паттерн «Фабрика» (уведомления)";

    public string Description =>
        "Реализовать фабрику уведомлений.";

    public string[] Requirements => new[]
    {
        "Интерфейс INotification с методом Send(string message)",
        "Реализации: EmailNotification, SmsNotification, PushNotification",
        "NotificationFactory.Create(NotificationType type) — возвращает нужную реализацию",
        "enum NotificationType { Email, Sms, Push }",
        "Продемонстрировать создание и отправку через фабрику"
    };

    public void Run()
    {
        string testMessage = "Привет! Это тестовое уведомление.";

        // Отправка через Email
        var email = NotificationFactory.Create(NotificationType.Email);
        email.Send(testMessage);
        Console.WriteLine();

        // Отправка через SMS
        var sms = NotificationFactory.Create(NotificationType.Sms);
        sms.Send(testMessage);
        Console.WriteLine();

        // Отправка через Push
        var push = NotificationFactory.Create(NotificationType.Push);
        push.Send(testMessage);
    }
}

// Перечисление типов уведомлений
public enum NotificationType
{
    Email,
    Sms,
    Push
}

// Интерфейс
public interface INotification
{
    void Send(string message);
}

// Реализации
public class EmailNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"[EMAIL] Отправка на почту: {message}");
    }
}

public class SmsNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"[SMS] Отправка на телефон: {message}");
    }
}

public class PushNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine($"[PUSH] Отправка пуш-уведомления: {message}");
    }
}

// Фабрика
public static class NotificationFactory
{
    public static INotification Create(NotificationType type)
    {
        return type switch
        {
            NotificationType.Email => new EmailNotification(),
            NotificationType.Sms => new SmsNotification(),
            NotificationType.Push => new PushNotification(),
            _ => throw new ArgumentException("Неизвестный тип уведомления")
        };
    }
}