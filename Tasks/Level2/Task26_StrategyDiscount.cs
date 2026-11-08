using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

[TaskInfo(26, 2)]
public sealed class Task26_StrategyDiscount : ITask
{
    public string Title => "Паттерн «Стратегия» (скидки)";

    public string Description =>
        "Реализовать калькулятор скидок через паттерн «Стратегия».";

    public string[] Requirements => new[]
    {
        "Интерфейс IDiscountStrategy с методом Apply(decimal price)",
        "Реализации: NoDiscount, VipDiscount (10%), PromoDiscount (20%)",
        "Класс PriceCalculator, принимающий стратегию в конструкторе",
        "Продемонстрировать расчёт цены с каждой стратегией"
    };

    public void Run()
    {
        decimal basePrice = 1000m;

        Console.WriteLine($"Базовая цена: {basePrice:C}");
        Console.WriteLine("--------------------------------------------------");

        // Применяем разные стратегии
        var calculator1 = new PriceCalculator(new NoDiscount());
        Console.WriteLine($"Без скидки: {calculator1.Calculate(basePrice):C}");

        var calculator2 = new PriceCalculator(new VipDiscount());
        Console.WriteLine($"VIP скидка (10%): {calculator2.Calculate(basePrice):C}");

        var calculator3 = new PriceCalculator(new PromoDiscount());
        Console.WriteLine($"Промо скидка (20%): {calculator3.Calculate(basePrice):C}");
    }
}

// Интерфейс стратегии
public interface IDiscountStrategy
{
    decimal Apply(decimal price);
}

// Конкретные стратегии
public class NoDiscount : IDiscountStrategy
{
    public decimal Apply(decimal price) => price;
}

public class VipDiscount : IDiscountStrategy
{
    public decimal Apply(decimal price) => price * 0.9m; // Скидка 10%
}

public class PromoDiscount : IDiscountStrategy
{
    public decimal Apply(decimal price) => price * 0.8m; // Скидка 20%
}

// Контекст (Калькулятор), который использует стратегию
public class PriceCalculator
{
    private readonly IDiscountStrategy _strategy;

    // Стратегия передается через конструктор
    public PriceCalculator(IDiscountStrategy strategy)
    {
        _strategy = strategy;
    }

    public decimal Calculate(decimal price)
    {
        return _strategy.Apply(price);
    }
}