using CSharpPracticum.Common;
using System.Diagnostics;

namespace CSharpPracticum.Tasks.Level1;

[TaskInfo(10, 1)]
public sealed class Task10_Sorting : ITask
{
    public string Title => "Сортировка пузырьком + вставками";

    public string Description =>
        "Отсортировать массив двумя алгоритмами и замерить время.";

    public string[] Requirements => new[]
    {
        "Реализовать BubbleSort и InsertionSort отдельными методами",
        "Оба — без LINQ и Array.Sort",
        "Замерить время каждого через Stopwatch на массиве 10 000 элементов",
        "Вывести оба времени"
    };

    public void Run()
    {
        const int size = 10000;
        Random rnd = new Random();

        // Создаем два одинаковых исходных массива
        int[] array1 = new int[size];
        for (int i = 0; i < size; i++)
            array1[i] = rnd.Next(1, 10000);

        // Копируем массив для второго алгоритма
        int[] array2 = (int[])array1.Clone();

        // --- Сортировка пузырьком ---
        Stopwatch sw1 = Stopwatch.StartNew();
        BubbleSort(array1);
        sw1.Stop();

        // --- Сортировка вставками ---
        Stopwatch sw2 = Stopwatch.StartNew();
        InsertionSort(array2);
        sw2.Stop();

        // Проверка, что оба массива отсортированы одинаково
        bool equal = true;
        for (int i = 0; i < size; i++)
        {
            if (array1[i] != array2[i])
            {
                equal = false;
                break;
            }
        }

        Console.WriteLine($"Время BubbleSort: {sw1.ElapsedMilliseconds} мс");
        Console.WriteLine($"Время InsertionSort: {sw2.ElapsedMilliseconds} мс");
        Console.WriteLine($"Массивы отсортированы одинаково: {equal}");
    }

    private void BubbleSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    // Swap (обмен)
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }

    private void InsertionSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 1; i < n; i++)
        {
            int key = arr[i];
            int j = i - 1;

            // Сдвигаем элементы, которые больше key, на одну позицию вправо
            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
            }
            arr[j + 1] = key;
        }
    }
}