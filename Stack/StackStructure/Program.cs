MyStack<int> lifo = new MyStack<int>(5);

for (int i = 1; i <= 5; i++)
{
    lifo.Push(i);
}

while (!lifo.IsEmpty())
{
    Console.WriteLine(lifo.Pop());
}
Console.WriteLine(lifo.Pop());

interface ImyStack<T>
{
    void Push(T item);
    T Pop();
    bool IsEmpty();
    bool IsFull();
}

class MyStack<T> : ImyStack<T>
{
    private T[] _items;
    private int _sp = -1;

    public MyStack(int size)
    {
        _items = new T[size];
    }

    public void Push(T item)
    {
        if (_sp == _items.Length - 1)
        {
            throw new StackOverflowException("Stack is full");
        }
        _items[++_sp] = item;
    }

    public T Pop()
    {
        if (_sp == -1)
        {
            throw new InvalidOperationException("Stack is empty");
        }
        return _items[_sp--];
    }

    public bool IsEmpty()
    {
        return _sp == -1;
    }

    public bool IsFull()
    {
        return _sp == _items.Length - 1;
    }
}
