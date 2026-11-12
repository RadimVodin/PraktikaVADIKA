using CSharpPracticum.Common;
using System.Diagnostics;

namespace CSharpPracticum.Tasks.Level3;

[TaskInfo(38, 3)]
public sealed class Task38_ListVsLinkedList : ITask
{
    public string Title => "List<T> vs LinkedList<T> (бенчмарк)";

    public string Description =>
        "Сравнить вставку в середину List<T> и LinkedList<T>.";

    public string[] Requirements => new[]
    {
        "Создать List<int> и LinkedList<int> по 100 000 элементов",
        "Вставить 1000 элементов в середину каждой коллекции",
        "Замерить время через Stopwatch",
        "Вывести оба времени и объяснить результат в комментарии"
    };

    public void Run()
    {
        const int size = 100_000;
        const int insertCount = 1000;

        Console.WriteLine($"Создание коллекций по {size} элементов...");

        List<int> list = new List<int>(size);
        LinkedList<int> linkedList = new LinkedList<int>();

        for (int i = 0; i < size; i++)
        {
            list.Add(i);
            linkedList.AddLast(i);
        }

        Console.WriteLine($"Начинаем вставку {insertCount} элементов в середину...\n");

        // --- Тест List<T> ---
        Stopwatch sw1 = Stopwatch.StartNew();
        for (int i = 0; i < insertCount; i++)
        {
            // Вставка в середину. List сдвигает все элементы вправо.
            list.Insert(list.Count / 2, -1);
        }
        sw1.Stop();

        // --- Тест LinkedList<T> ---
        Stopwatch sw2 = Stopwatch.StartNew();
        // Находим серединный узел один раз
        LinkedListNode<int>? middleNode = linkedList.First;
        for (int i = 0; i < size / 2; i++)
        {
            middleNode = middleNode!.Next;
        }

        for (int i = 0; i < insertCount; i++)
        {
            // Вставка в LinkedList происходит быстро (меняются только ссылки),
            // НО мы должны передать узел. Мы вставляем относительно одного и того же узла.
            linkedList.AddBefore(middleNode!, -1);
        }
        sw2.Stop();

        Console.WriteLine($"Время List<T>:       {sw1.ElapsedMilliseconds} мс");
        Console.WriteLine($"Время LinkedList<T>: {sw2.ElapsedMilliseconds} мс");

        /*
         * ОБЪЯСНЕНИЕ РЕЗУЛЬТАТА:
         * 
         * 1. List<T> внутри использует обычный массив. Когда мы вставляем элемент 
         *    в середину, все элементы после него сдвигаются на одну позицию вправо.
         *    Это занимает O(n) времени. На 100 000 элементах это долго.
         * 
         * 2. LinkedList<T> использует двусвязный список. Вставка узла в середину 
         *    занимает O(1), так как нужно только поменять ссылки (prev/next).
         *    Но чтобы найти саму середину, нужно пройти O(n) элементов (мы это сделали 
         *    один раз до начала цикла).
         * 
         * В ИТОГЕ: LinkedList быстрее на вставке, но медленнее на поиске по индексу.
         */
    }
}