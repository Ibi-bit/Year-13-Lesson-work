CQueue<int> queue = new CQueue<int>(5);
queue.Enqueue(10);
queue.Enqueue(20);
queue.Enqueue(30);
Console.WriteLine(queue.Dequeue());
Console.WriteLine(queue.Peek());

interface ICQueue<T>
{
    void Enqueue(T item);
    T Dequeue();
    T Peek();
    bool IsEmpty();
    int Size();
}

public class CQueue<T> : ICQueue<T>
{
    private T[] items;
    private int front;
    private int rear;
    private int count;

    public CQueue(int capacity)
    {
        items = new T[capacity];
        front = 0;
        rear = 0;
        count = 0;
    }

    public void Enqueue(T item)
    {
        if (count == items.Length)
            throw new InvalidOperationException("Queue is full");

        items[rear] = item;
        rear = (rear + 1) % items.Length;
        count++;
    }

    public T Dequeue()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Queue is empty");

        T item = items[front];
        front = (front + 1) % items.Length;
        count--;
        return item;
    }

    public T Peek()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Queue is empty");

        return items[front];
    }

    public bool IsEmpty()
    {
        return count == 0;
    }

    public int Size()
    {
        return count;
    }
}
