using System.IO.Pipelines;

Console.WriteLine("Dynamic Queue Implementation");
DynamicQueue<int> queue = new DynamicQueue<int>();
queue.Add(10);
queue.Add(20);
Console.WriteLine(queue.Peak()); // Output: 10
Console.WriteLine(queue.Remove()); // Output: 10
Console.WriteLine(queue.Peak()); // Output: 20
Console.WriteLine(queue.Remove()); // Output: 20

for (int i = 1; i <= 50; i++)
{
    queue.Add(i * 100);
}
for (int i = 1; i <= 50; i++)
{
    Console.WriteLine(queue.Remove());
}
for (int i = 51; i <= 100; i++)
{
    queue.Add(i * 100);
}
for (int i = 51; i <= 100; i++)
{
    Console.WriteLine(queue.Remove());
}

Console.WriteLine("\n=== QueueJump Priority Test ===");
QueueJump<string> priorityQueue = new QueueJump<string>();

Console.WriteLine("Adding items...");
priorityQueue.Add("Low priority task", 10);
Console.WriteLine("Added: Low priority (0)");
priorityQueue.Add("High priority task", 0);
Console.WriteLine("Added: High priority (10)");
priorityQueue.Add("Medium priority task", 5);
Console.WriteLine("Added: Medium priority (5)");

Console.WriteLine("Removing items:");
try
{
    Console.WriteLine(priorityQueue.Remove());
    Console.WriteLine(priorityQueue.Remove());
    Console.WriteLine(priorityQueue.Remove());
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

public class DynamicQueue<T>
{
    protected Node? front;
    protected Node? rear;
    protected int size;

    protected class Node
    {
        public T _data;
        public Node? _ptr;

        public Node(T data)
        {
            _data = data;
            _ptr = null;
        }
    }

    public DynamicQueue()
    {
        front = null;
        rear = null;
        size = 0;
    }

    public virtual void Add(T data, int? priority = 0)
    {
        Node temp = new Node(data);
        if (size == 0)
        {
            front = temp;
        }
        else
        {
            rear._ptr = temp;
        }
        rear = temp;
        size++;
    }

    public T Peak()
    {
        if (size == 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }
        return front._data;
    }

    public T Remove()
    {
        T data = Peak();
        if (size == 1)
        {
            front = null;
            rear = null;
        }
        else
        {
            front = front._ptr;
        }
        size--;

        return data;
    }
}

class QueueJump<T> : DynamicQueue<T>
{
    PriorityNode? front;
    PriorityNode? rear;

    public QueueJump()
        : base()
    {
        front = null;
        rear = null;
    }

    protected class PriorityNode : Node
    {
        public PriorityNode? _ptr;
        public int? _priority;

        ///<summary>
        /// Priority Node Constructor
        /// 0 = highest priority
        ///</summary>
        public PriorityNode(T data, int? priority)
            : base(data)
        {
            _priority = priority;
        }
    }

    public override void Add(T data, int? priority)
    {
        PriorityNode temp = new PriorityNode(data, priority);
        if (size == 0)
        {
            front = temp;
            rear = temp;
        }
        else if (priority <= front._priority)
        {
            temp._ptr = front;
            front = temp;
        }
        else if (priority >= rear._priority)
        {
            rear._ptr = temp;
            rear = temp;
        }
        else
        {
            bool foundQueuePlace = false;
            PriorityNode firstHalf;
            PriorityNode secondHalf = front;
            while (!foundQueuePlace)
            {
                if (temp._priority > secondHalf._priority && priority > secondHalf._ptr._priority)
                {
                    firstHalf = secondHalf;
                    secondHalf = secondHalf._ptr;
                    firstHalf._ptr = temp;
                    temp._ptr = secondHalf;
                    foundQueuePlace = true;
                }
                else
                {
                    firstHalf = secondHalf;
                    secondHalf = secondHalf._ptr;
                }
            }
        }
        size++;
    }

    public new T Remove()
    {
        if (size == 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }

        T data = front._data;
        if (size == 1)
        {
            front = null;
            rear = null;
        }
        else
        {
            front = front._ptr;
        }
        size--;
        return data;
    }

    public new T Peak()
    {
        if (size == 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }
        return front._data;
    }
}
