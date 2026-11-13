using CSharpPracticum.Common;
using CSharpPracticum.Tasks.Level1;
using CSharpPracticum.Tasks.Level2;
using CSharpPracticum.Tasks.Level3;

namespace CSharpPracticum.Menu;

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
                        (25, () => new Task25_Shapes()),
                        (26, () => new Task26_StrategyDiscount()),
                        (27, () => new Task27_StudentSorting()),
                        (28, () => new Task28_PointEquals()),
                        (29, () => new Task29_FactoryNotification()),
                        (30, () => new Task30_BankAccount()),
                        (31, () => new Task31_DisposableFileWriter()),
                        (32, () => new Task32_TemperatureValidation()),
                        (33, () => new Task33_LinqEmployees()),
                        (34, () => new Task34_LinqWordGroups()),
                    }).Show();
                    break;

                case "3":
                    new LevelMenu("Уровень 3: Продвинутый", new (int, Func<ITask>)[]
                    {
                        (36, () => new Task36_Duplicates()),
                        (37, () => new Task37_Memoization()),
                        (38, () => new Task38_ListVsLinkedList()),
                        (39, () => new Task39_YieldGenerator()),
                        (40, () => new Task40_AsyncAwait()),
                        (41, () => new Task41_WhenAll()),
                        (42, () => new Task42_Cancellation()), // <--- Добавлено задание 42
                    }).Show();
                    break;

                default:
                    Console.WriteLine("Некорректный ввод.");
                    break;
            }
        }
    }
}