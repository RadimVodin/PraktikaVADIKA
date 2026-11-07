using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level1;

[TaskInfo(11, 1)]
public sealed class Task11_MatrixDiagonal : ITask
{
    public string Title => "Диагональ матрицы N×N";

    public string Description =>
        "Найти сумму элементов главной диагонали матрицы N×N.";

    public string[] Requirements => new[]
    {
        "Размер N вводится пользователем (минимум 2)",
        "Матрица заполняется случайными числами",
        "Сумма считается без LINQ, в одном цикле for",
        "Вывести саму матрицу и сумму"
    };

    public void Run()
    {
        Console.Write("Введите размер матрицы N (минимум 2): ");
        string? input = Console.ReadLine();

        if (!int.TryParse(input, out int n) || n < 2)
        {
            Console.WriteLine("Ошибка: размер должен быть целым числом не меньше 2.");
            return;
        }

        int[,] matrix = new int[n, n];
        Random rnd = new Random();

        // Заполняем матрицу
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = rnd.Next(1, 10); // числа от 1 до 9 для красоты
            }
        }

        // Выводим матрицу
        Console.WriteLine("\nМатрица:");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write($"{matrix[i, j],4}");
            }
            Console.WriteLine();
        }

        // Считаем сумму главной диагонали в одном цикле
        int sum = 0;
        for (int i = 0; i < n; i++)
        {
            sum += matrix[i, i];
        }

        Console.WriteLine($"\nСумма элементов главной диагонали: {sum}");
    }
}