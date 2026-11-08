using CSharpPracticum.Common;
using CSharpPracticum.Tasks.Level1;
using CSharpPracticum.Tasks.Level2;

namespace CSharpPracticum.Menu;

/// <summary>
/// Главное меню.
/// </summary>
public sealed class MenuRunner
{
    public void Run()
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== Практикум по C# ===");
            Console.WriteLine("1. Уровень 1 — Базовый (задания 1–15)");
            Console.WriteLine("2. Уровень 2 — Средний (задания 16–35)");
            Console.WriteLine("3. Уровень 3 — Продвинутый (задания 36–50)");
            Console.WriteLine("4. Выход");
            Console.Write("Выберите уровень: ");

            string? input = Console.ReadLine();
            if (input == "0" || input == "q" || input == "4")
                return;

            switch (input)
            {
                case "1":
                    new LevelMenu("Уровень 1: Базовый", new (int, Func<ITask>)[]
                    {
                        (1, () => new Task01_HelloWorld()),
                        (2, () => new Task02_Primitives()),
                        (3, () => new Task03_Calculator()),
                        (4, () => new Task04_ParityCheck()),
                        (5, () => new Task05_MultiplicationTable()),
                        (6, () => new Task06_DigitSum()),
                        (7, () => new Task07_StringReverse()),
                        (8, () => new Task08_FizzBuzz()),
                        (9, () => new Task09_ArrayStats()),
                        (10, () => new Task10_Sorting()),
                        (11, () => new Task11_MatrixDiagonal()),
                        (12, () => new Task12_Palindrome()),
                        (13, () => new Task13_WordCount()),
                        (14, () => new Task14_AgeCalculator()),
                    }).Show();
                    break;

                case "2":
                    new LevelMenu("Уровень 2: Средний", new (int, Func<ITask>)[]
                    {
                        (16, () => new Task16_StringExtensions()),
                        (17, () => new Task17_StringBuilderVsConcat()),
                        (18, () => new Task18_TryParseVsTryCatch()),
                        (19, () => new Task19_MyStackQueue()),
                        (20, () => new Task20_Generics()),
                        (21, () => new Task21_EnumDays()),
                        (22, () => new Task22_ParamsOverload()),
                        (23, () => new Task23_NullableTypes()),
                        (24, () => new Task24_AnimalHierarchy()),
                        (25, () => new Task25_Shapes()), // <--- Добавлено задание 25
                    }).Show();
                    break;

                case "3":
                    Console.WriteLine("Уровень 3 пока не реализован.");
                    break;

                default:
                    Console.WriteLine("Некорректный ввод.");
                    break;
            }
        }
    }
}