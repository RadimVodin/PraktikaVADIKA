using CSharpPracticum.Common;

namespace CSharpPracticum.Tasks.Level2;

[TaskInfo(19, 2)]
public sealed class Task19_MyStackQueue : ITask
{
    public string Title => "Свой стек/очередь на массиве";

    public string Description =>
        "Реализовать MyStack<T> и MyQueue<T> на массиве.";

    public string[] Requirements => new[]
    {
        "Стек: Push, Pop, Peek, Count, IsEmpty",
        "Очередь: Enqueue, Dequeue, Peek, Count, IsEmpty",
        "При переполнении — расширять внутренний массив (удвоение)",
        "Pop/Dequeue на пустой коллекции — бросать InvalidOperationException"
    };

    public void Run()
    {
        Console.WriteLine("--- Тестирование MyStack ---");
        MyStack<int> stack = new MyStack<int>();

        stack.Push(10);
        stack.Push(20);
        stack.Push(30);
        Console.WriteLine($"Стек: Push 10, 20, 30. Count = {stack.Count}");
        Console.WriteLine($"Peek: {stack.Peek()}");
        Console.WriteLine($"Pop: {stack.Pop()}");
        Console.WriteLine($"Pop: {stack.Pop()}");
        Console.WriteLine($"Count после двух Pop: {stack.Count}");
        Console.WriteLine($"IsEmpty: {stack.IsEmpty}");

        // Тест на пустой стек
        stack.Pop();
        try
        {
            stack.Pop();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Ожидаемое исключение: {ex.Message}");
        }

        Console.WriteLine("\n--- Тестирование MyQueue ---");
        MyQueue<string> queue = new MyQueue<string>();

        queue.Enqueue("A");
        queue.Enqueue("B");
        queue.Enqueue("C");
        Console.WriteLine($"Очередь: Enqueue A, B, C. Count = {queue.Count}");
        Console.WriteLine($"Peek: {queue.Peek()}");
        Console.WriteLine($"Dequeue: {queue.Dequeue()}");
        Console.WriteLine($"Dequeue: {queue.Dequeue()}");
        Console.WriteLine($"Count после двух Dequeue: {queue.Count}");
        Console.WriteLine($"IsEmpty: {queue.IsEmpty}");

        // Тест расширения массива (добавим 20 элементов)
        for (int i = 0; i < 20; i++) queue.Enqueue(i.ToString());
        Console.WriteLine($"После добавления 20 элементов Count = {queue.Count} (массив расширился)");
    }
}

// --- Реализация Стека (LIFO - Last In, First Out) ---
public class MyStack<T>
{
    private T[] _items;
    private int _count;

    public MyStack()
    {
        _items = new T[4]; // Начальная емкость
        _count = 0;
    }

    public int Count => _count;
    public bool IsEmpty => _count == 0;

    public void Push(T item)
    {
        if (_count == _items.Length)
        {
            // Расширяем массив в 2 раза
            T[] newArray = new T[_items.Length * 2];
            Array.Copy(_items, newArray, _items.Length);
            _items = newArray;
        }
        _items[_count] = item;
        _count++;
    }

    public T Pop()
    {
        if (IsEmpty) throw new InvalidOperationException("Стек пуст.");
        _count--;
        return _items[_count];
    }

    public T Peek()
    {
        if (IsEmpty) throw new InvalidOperationException("Стек пуст.");
        return _items[_count - 1];
    }
}

// --- Реализация Очереди (FIFO - First In, First Out) ---
public class MyQueue<T>
{
    private T[] _items;
    private int _head;
    private int _tail;
    private int _count;

    public MyQueue()
    {
        _items = new T[4];
        _head = 0;
        _tail = 0;
        _count = 0;
    }

    public int Count => _count;
    public bool IsEmpty => _count == 0;

    public void Enqueue(T item)
    {
        if (_count == _items.Length)
        {
            // Расширяем массив в 2 раза
            T[] newArray = new T[_items.Length * 2];
            // Копируем элементы в правильном порядке (начиная с _head)
            for (int i = 0; i < _count; i++)
            {
                newArray[i] = _items[(_head + i) % _items.Length];
            }
            _items = newArray;
            _head = 0;
            _tail = _count;
        }

        _items[_tail] = item;
        _tail = (_tail + 1) % _items.Length;
        _count++;
    }

    public T Dequeue()
    {
        if (IsEmpty) throw new InvalidOperationException("Очередь пуста.");

        T item = _items[_head];
        _head = (_head + 1) % _items.Length;
        _count--;
        return item;
    }

    public T Peek()
    {
        if (IsEmpty) throw new InvalidOperationException("Очередь пуста.");
        return _items[_head];
    }
}