using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level1;

[TaskInfo(2, 1)]
public sealed class Task02_Primitives : ITask
{
    public string Title => "Переменные примитивных типов";

    public string Description =>
        "Объявить переменные всех примитивных типов и вывести их значения и типы.";

    public string[] Requirements => new[]
    {
        "Объявить минимум: int, long, double, float, decimal, bool, char, string",
        "Для каждой переменной вывести значение и GetType().Name",
        "decimal — с суффиксом m, float — с f"
    };

    public void Run()
    {
        int myInt = 42;
        long myLong = 9000000000L;
        double myDouble = 3.14159;
        float myFloat = 2.718f;
        decimal myDecimal = 99.99m;
        bool myBool = true;
        char myChar = 'A';
        string myString = "Привет";

        Console.WriteLine($"int: {myInt} ({myInt.GetType().Name})");
        Console.WriteLine($"long: {myLong} ({myLong.GetType().Name})");
        Console.WriteLine($"double: {myDouble} ({myDouble.GetType().Name})");
        Console.WriteLine($"float: {myFloat} ({myFloat.GetType().Name})");
        Console.WriteLine($"decimal: {myDecimal} ({myDecimal.GetType().Name})");
        Console.WriteLine($"bool: {myBool} ({myBool.GetType().Name})");
        Console.WriteLine($"char: {myChar} ({myChar.GetType().Name})");
        Console.WriteLine($"string: {myString} ({myString.GetType().Name})");
    }
}