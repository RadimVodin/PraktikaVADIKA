using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

[TaskInfo(30, 2)]
public sealed class Task30_BankAccount : ITask
{
    public string Title => "BankAccount + свой Exception";

    public string Description =>
        "Реализовать банковский счёт с собственным исключением.";

    public string[] Requirements => new[]
    {
        "Класс BankAccount с приватным балансом",
        "Методы Deposit(decimal), Withdraw(decimal)",
        "Свой класс InsufficientFundsException : Exception",
        "Withdraw бросает исключение, если сумма больше баланса",
        "Продемонстрировать: успешное снятие и попытку снять больше баланса (с try/catch)"
    };

    public void Run()
    {
        BankAccount account = new BankAccount(1000m);
        Console.WriteLine($"Начальный баланс: {account.GetBalance():C}");

        // Успешное пополнение и снятие
        account.Deposit(500m);
        Console.WriteLine($"После пополнения на 500: {account.GetBalance():C}");

        account.Withdraw(200m);
        Console.WriteLine($"После снятия 200: {account.GetBalance():C}");
        Console.WriteLine();

        // Попытка снять больше, чем есть (должно выбросить исключение)
        try
        {
            account.Withdraw(5000m);
        }
        catch (InsufficientFundsException ex)
        {
            Console.WriteLine($"ОШИБКА: {ex.Message}");
            Console.WriteLine($"Не хватило: {ex.MissingAmount:C}");
        }

        Console.WriteLine($"\nИтоговый баланс (не должен уйти в минус): {account.GetBalance():C}");
    }
}

// Собственное исключение
public class InsufficientFundsException : Exception
{
    public decimal MissingAmount { get; }

    public InsufficientFundsException(string message, decimal missingAmount) : base(message)
    {
        MissingAmount = missingAmount;
    }
}

// Класс счета
public class BankAccount
{
    private decimal _balance;

    public BankAccount(decimal initialBalance)
    {
        _balance = initialBalance;
    }

    public decimal GetBalance() => _balance;

    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Сумма пополнения должна быть больше нуля.");
        _balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Сумма снятия должна быть больше нуля.");

        if (amount > _balance)
        {
            decimal missing = amount - _balance;
            throw new InsufficientFundsException(
                $"Попытка снять {amount:C}, но на счету только {_balance:C}.",
                missing);
        }

        _balance -= amount;
    }
}