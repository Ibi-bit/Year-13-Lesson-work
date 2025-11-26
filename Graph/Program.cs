using Microsoft.VisualBasic;

Graph<char> graph = new Graph<char>();
graph.Add('A');
graph.Add('B');
graph.Add('E');
graph.Add('F');
graph.Add('G');
graph.Add('H');
graph.Add('K');
graph.Add('M');

graph.BiDirectionalEdge('A', 'B');
graph.BiDirectionalEdge('A', 'F');
graph.BiDirectionalEdge('A', 'H');
graph.BiDirectionalEdge('B', 'H');
graph.BiDirectionalEdge('B', 'G');
graph.BiDirectionalEdge('B', 'E');
graph.BiDirectionalEdge('E', 'G');
graph.BiDirectionalEdge('E', 'M');
graph.BiDirectionalEdge('E', 'F');
graph.BiDirectionalEdge('H', 'G');
graph.BiDirectionalEdge('H', 'K');
graph.BiDirectionalEdge('K', 'M');
graph.BiDirectionalEdge('M', 'G');

var adjacencyList = graph.CreateAdjacencyList();
foreach (var kvp in adjacencyList)
{
    Console.Write($"{kvp.Key}: ");
    foreach (var neighbor in kvp.Value)
    {
        Console.Write($"{neighbor} ");
    }
    Console.WriteLine();
}

public class Graph<T>
    where T : notnull
{
    private class Node
    {
        public T Value;
        public List<Node> Neighbors;

        public Node(T value)
        {
            Value = value;
            Neighbors = new List<Node>();
        }
    }

    private Dictionary<T, Node> Nodes = new Dictionary<T, Node>();

    public Graph()
    {
        Nodes = new Dictionary<T, Node>();
    }

    public void Add(T value)
    {
        if (!Nodes.ContainsKey(value))
        {
            Nodes[value] = new Node(value);
        }
    }

    public void AddEdge(T from, T to)
    {
        if (Nodes.ContainsKey(from) && Nodes.ContainsKey(to))
        {
            Nodes[from].Neighbors.Add(Nodes[to]);
        }
    }

    public void BiDirectionalEdge(T from, T to)
    {
        AddEdge(from, to);
        AddEdge(to, from);
    }

    public void RemoveEdge(T from, T to)
    {
        if (Nodes.ContainsKey(from) && Nodes.ContainsKey(to))
        {
            Nodes[from].Neighbors.Remove(Nodes[to]);
        }
    }

    public void RemoveBiDirectionalEdge(T from, T to)
    {
        RemoveEdge(from, to);
        RemoveEdge(to, from);
    }

    public List<T> GetNeighbors(T value)
    {
        if (Nodes.ContainsKey(value))
        {
            return Nodes[value].Neighbors.Select(n => n.Value).ToList();
        }
        return new List<T>();
    }

    public Dictionary<T, List<T>> CreateAdjacencyList()
    {
        var adjacencyList = new Dictionary<T, List<T>>();
        foreach (var node in Nodes.Values)
        {
            adjacencyList[node.Value] = GetNeighbors(node.Value);
        }
        return adjacencyList;
    }
    public List<T> DepthFirstSearch(T value)
    {
        foreach
        
    }
    private List<Node> DepthFirstSearchRec(List<T> values, List<Node> )
}
