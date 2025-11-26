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
var dfsResult = graph.DepthFirstSearch('A', 'M');
Console.WriteLine($"Depth First Search from 'A' to 'M': {string.Join(" -> ", dfsResult)}");

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

    public List<T> DepthFirstSearch(T from, T to)
    {
        var path = new List<T>();
        var visited = new HashSet<Node>();

        if (!Nodes.ContainsKey(from) || !Nodes.ContainsKey(to))
            return path;

        if (DepthFirstSearchRec(Nodes[from], Nodes[to], path, visited))
            return path;
        return new List<T>();
    }

    private bool DepthFirstSearchRec(Node current, Node target, List<T> path, HashSet<Node> visited)
    {
        visited.Add(current);
        path.Add(current.Value);
        if (current == target)
            return true;
        foreach (var neighbor in current.Neighbors)
        {
            if (!visited.Contains(neighbor))
            {
                if (DepthFirstSearchRec(neighbor, target, path, visited))
                    return true;
            }
        }

        path.RemoveAt(path.Count - 1);
        return false;
    }
}
