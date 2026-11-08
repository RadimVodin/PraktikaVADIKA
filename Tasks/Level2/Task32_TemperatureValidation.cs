using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

[TaskInfo(32, 2)]
public sealed class Task32_TemperatureValidation : ITask
{
    public string Title => "Свойства с валидацией (Temperature)";

    public string Description =>
        "Класс Temperature с валидацией в set.";

    public string[] Requirements => new[]
    {
        "Свойство Celsius с проверкой в set: не ниже −273.15",
        "При некорректном значении — бросать ArgumentOutOfRangeException",
        "Вычисляемое свойство Fahrenheit (только get)",
        "Продемонстрировать корректное и некорректное присваивание"
    };

    public void Run()
    {
        Temperature temp = new Temperature();

        // 1. Корректное присваивание
        Console.WriteLine("--- Корректное присваивание ---");
        temp.Celsius = 25.0;
        Console.WriteLine($"Установлено: {temp.Celsius} °C");
        Console.WriteLine($"Пересчет в Фаренгейты: {temp.Fahrenheit:F2} °F\n");

        temp.Celsius = 0.0;
        Console.WriteLine($"Установлено: {temp.Celsius} °C");
        Console.WriteLine($"Пересчет в Фаренгейты: {temp.Fahrenheit:F2} °F\n");

        // 2. Некорректное присваивание (ниже абсолютного нуля)
        Console.WriteLine("--- Некорректное присваивание ---");
        try
        {
            temp.Celsius = -300.0;
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine("Ошибка: поймано исключение ArgumentOutOfRangeException");
            Console.WriteLine($"Сообщение: {ex.Message}");
            // Проверяем, что старое значение не изменилось
            Console.WriteLine($"Текущее значение осталось прежним: {temp.Celsius} °C");
        }
    }
}

public class Temperature
{
    private double _celsius;

    public double Celsius
    {
        get => _celsius;
        set
        {
            // Валидация в сеттере
            if (value < -273.15)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value),
                    "Температура не может быть ниже абсолютного нуля (-273.15 °C).");
            }
            _celsius = value;
        }
    }

    // Вычисляемое свойство (только get)
    public double Fahrenheit
    {
        get => (_celsius * 9 / 5) + 32;
    }
}