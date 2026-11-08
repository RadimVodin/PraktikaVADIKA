using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

[TaskInfo(23, 2)]
public sealed class Task23_NullableTypes : ITask
{
    public string Title => "Nullable-типы, ??, ?.";

    public string Description =>
        "Продемонстрировать работу с nullable.";

    public string[] Requirements => new[]
    {
        "Метод int? FindValue(string key) — возвращает null, если ключа нет",
        "Использовать ?? для значения по умолчанию",
        "Использовать ?. для безопасного доступа к свойству объекта, который может быть null",
        "Использовать HasValue / Value с проверкой",
        "Продемонстрировать на примере с найденным и ненайденным ключом"
    };

    // Класс для демонстрации ?. (может быть null)
    public class User
    {
        public string Name { get; set; } = "";
    }

    public void Run()
    {
        // Имитация базы данных
        var db = new Dictionary<string, int>
        {
            { "apple", 10 },
            { "banana", 20 }
        };

        Console.WriteLine("--- Тест 1: Найденный ключ ---");
        int? value1 = FindValue(db, "apple");

        // Использование HasValue / Value
        if (value1.HasValue)
        {
            Console.WriteLine($"Ключ 'apple' найден. Значение: {value1.Value}");
        }
        else
        {
            Console.WriteLine("Ключ не найден.");
        }

        // Использование ?? (если null, то 0)
        int result1 = FindValue(db, "apple") ?? 0;
        Console.WriteLine($"Использование ??: {result1}");

        Console.WriteLine("\n--- Тест 2: Ненайденный ключ ---");
        int? value2 = FindValue(db, "orange");

        if (value2.HasValue)
        {
            Console.WriteLine($"Ключ найден. Значение: {value2.Value}");
        }
        else
        {
            Console.WriteLine("Ключ 'orange' не найден (вернулся null).");
        }

        // Использование ?? (если null, то -1)
        int result2 = FindValue(db, "orange") ?? -1;
        Console.WriteLine($"Использование ??: {result2}");

        Console.WriteLine("\n--- Тест 3: Оператор ?. (безопасный доступ) ---");

        User? user1 = new User { Name = "Вадим" };
        User? user2 = null;

        // Безопасно получаем длину имени. Если user1 равен null, вернется null.
        int? len1 = user1?.Name.Length;
        Console.WriteLine($"Длина имени user1: {len1 ?? 0}");

        // user2 равен null. Без ?. программа бы упала с NullReferenceException.
        int? len2 = user2?.Name.Length;
        Console.WriteLine($"Длина имени user2: {len2 ?? 0}");
    }

    // Метод возвращает int? (nullable int)
    private int? FindValue(Dictionary<string, int> db, string key)
    {
        if (db.TryGetValue(key, out int value))
        {
            return value;
        }
        return null; // Ключа нет — возвращаем null
    }
}