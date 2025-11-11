using System;
using System.Collections.Generic;

Console.WriteLine("=== Linear Queue Test ===");

LinearQueue<int> queue = new LinearQueue<int>(5);

Console.WriteLine("Adding 10, 20, 30");
queue.Enqueue(10);
queue.Enqueue(20);
queue.Enqueue(30);
Console.WriteLine($"Size: {queue.Size()}");
Console.WriteLine($"Peek: {queue.Peek()}");

Console.WriteLine("\nRemoving items:");
Console.WriteLine($"Dequeue: {queue.Dequeue()}");
Console.WriteLine($"Dequeue: {queue.Dequeue()}");
Console.WriteLine($"Size after removing 2: {queue.Size()}");
Console.WriteLine($"Peek remaining: {queue.Peek()}");

Console.WriteLine("\nAdding 40, 50:");
queue.Enqueue(40);
queue.Enqueue(50);
Console.WriteLine($"Size: {queue.Size()}");

Console.WriteLine("\nRemoving all:");
Console.WriteLine($"Dequeue: {queue.Dequeue()}");
Console.WriteLine($"Dequeue: {queue.Dequeue()}");
Console.WriteLine($"Dequeue: {queue.Dequeue()}");
Console.WriteLine($"Is empty: {queue.IsEmpty()}");

Console.WriteLine("\nTesting full queue:");
for (int i = 1; i <= 5; i++)
{
    queue.Enqueue(i * 10);
}
Console.WriteLine($"Queue full, size: {queue.Size()}");

try
{
    queue.Enqueue(999);
}
catch (Exception ex)
{
    Console.WriteLine($"Error adding to full queue: {ex.Message}");
}

interface ILinearQueue<T>
{
    void Enqueue(T item);
    T Dequeue();
    T Peek();
    bool IsEmpty();
    int Size();
}

class LinearQueue<T> : ILinearQueue<T>
{
    private T[] items;
    private int front;
    private int rear;

    public LinearQueue(int capacity)
    {
        items = new T[capacity];
        front = 0;
        rear = 0;
    }




    public void Enqueue(T item)
    {
        if (Size() == items.Length)
        {
            throw new InvalidOperationException("Queue is full");
        }
        items[rear] = item;
        rear++;
    }

    public T Dequeue()
    {
        IsEmptyException();
        T item = items[0];

        for (int i = 0; i < Size() - 1; i++)
        {
            items[i] = items[i + 1];
        }

        rear--;

        return item;
    }

    public T Peek()
    {
        IsEmptyException();
        return items[front];
    }

    public bool IsEmpty()
    {
        return Size() == 0;
    }

    public int Size()
    {
        return rear - front;
    }

    private void IsEmptyException()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Queue is empty");
    }
}
