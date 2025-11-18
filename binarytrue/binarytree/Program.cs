Console.WriteLine("=== Binary Tree Test ===");

BinaryTree<int> tree = new BinaryTree<int>();

Console.WriteLine("Inserting values: 50, 30, 70, 20, 40, 60, 80");
tree.Insert(50);
tree.Insert(30);
tree.Insert(70);
tree.Insert(20);
tree.Insert(40);
tree.Insert(60);
tree.Insert(80);
tree.Insert(-70); // Duplicate, should be ignored

Console.WriteLine("\nSearching for values:");
Console.WriteLine($"Search 30: {tree.Search(30)}"); // Should be true
Console.WriteLine($"Search 40: {tree.Search(40)}"); // Should be true
Console.WriteLine($"Search 25: {tree.Search(25)}"); // Should be false
Console.WriteLine($"Search 80: {tree.Search(80)}"); // Should be true
Console.WriteLine($"Search 100: {tree.Search(100)}"); // Should be false

Console.WriteLine("\nTesting edge cases:");
Console.WriteLine($"Search root (50): {tree.Search(50)}"); // Should be true
Console.WriteLine($"Search smallest (20): {tree.Search(20)}"); // Should be true
Console.WriteLine($"Search largest (80): {tree.Search(80)}"); // Should be true

Console.WriteLine("\nTesting duplicates:");
tree.Insert(50);
Console.WriteLine($"Search 50 after duplicate insert: {tree.Search(50)}"); // Still true
List<int> values = tree.TraversalInOrder();
Console.WriteLine($"Tree values (in-order): {string.Join(", ", values)}");

class BinaryTree<T>
    where T : IComparable<T>
{
    private class Node
    {
        public T Data;
        public Node? Left;
        public Node? Right;

        public Node(T data)
        {
            Data = data;
            Left = null;
            Right = null;
        }
    }

    private Node? root;

    public BinaryTree()
    {
        root = null;
    }

    public void Insert(T data)
    {
        if (root == null)
        {
            root = new Node(data);
        }
        else
        {
            if (data.CompareTo(root.Data) < 0)
            {
                root.Left = InsertRec(root.Left, data);
            }
            else
            {
                root.Right = InsertRec(root.Right, data);
            }
        }
    }

    private Node? InsertRec(Node? node, T data)
    {
        if (node == null)
        {
            node = new Node(data);
            return node;
        }
        if (data.CompareTo(node.Data) < 0)
        {
            node.Left = InsertRec(node.Left, data);
        }
        else if (data.CompareTo(node.Data) > 0)
        {
            node.Right = InsertRec(node.Right, data);
        }
        return node;
    }

    private bool SearchRec(Node? node, T data)
    {
        if (node == null)
            return false;
        if (data.CompareTo(node.Data) < 0)
        {
            return SearchRec(node.Left, data);
        }
        else if (data.CompareTo(node.Data) > 0)
        {
            return SearchRec(node.Right, data);
        }
        else
        {
            return true;
        }
    }

    public bool Search(T data)
    {
        return SearchRec(root, data);
    }

    public List<T> TraversalInOrder()
    {
        var (_, values) = TraversalInOrder(root);
        return values;
    }

    private (Node?, List<T>) TraversalInOrder(Node? node, List<T>? values = null)
    {
        values ??= new List<T>();
        if (node != null)
        {
            TraversalInOrder(node.Left, values);
            values.Add(node.Data);
            TraversalInOrder(node.Right, values);
        }
        return (node, values);
    }
}
