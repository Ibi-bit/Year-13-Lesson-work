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
graph.AddEdge('A', 'B');
graph.AddEdge('A', 'F');
graph.AddEdge('A', 'H');
graph.AddEdge('B', 'A');
graph.AddEdge('B', 'E');
graph.AddEdge('B', 'G');
graph.AddEdge('B', 'H');
graph.AddEdge('E', 'B');
graph.AddEdge('E', 'F');
graph.AddEdge('E', 'G');
graph.AddEdge('E', 'M');
graph.AddEdge('F', 'A');
graph.AddEdge('F', 'E');
graph.AddEdge('G', 'B');
graph.AddEdge('G', 'E');
graph.AddEdge('G', 'H');
graph.AddEdge('G', 'M');
graph.AddEdge('H', 'A');
graph.AddEdge('H', 'B');
graph.AddEdge('H', 'G');
graph.AddEdge('H', 'K');
graph.AddEdge('K', 'H');
graph.AddEdge('K', 'M');
graph.AddEdge('M', 'E');
graph.AddEdge('M', 'G');
graph.AddEdge('M', 'K');

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
var bfsResult = graph.BreadthFirstSearch('A', 'M');
Console.WriteLine("Breadth First Search levels from 'A':");
foreach (var level in bfsResult)
{
    Console.WriteLine($"Level {level.Key}: {string.Join(", ", level.Value)}");
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

    public Dictionary<int, List<T>> BreadthFirstSearch(T from, T to)
    {
        var queue = new Queue<Node>();
        var visited = new HashSet<T>();
        var levels = new Dictionary<int, List<T>>();
        var currentLevel = 0;

        if (!Nodes.ContainsKey(from))
            return levels;

        queue.Enqueue(Nodes[from]);
        visited.Add(from);
        levels[0] = new List<T> { from };

        while (queue.Count > 0)
        {
            var levelSize = queue.Count;
            var nextLevelNodes = new List<T>();

            for (int i = 0; i < levelSize; i++)
            {
                var current = queue.Dequeue();

                foreach (var neighbor in current.Neighbors)
                {
                    if (!visited.Contains(neighbor.Value))
                    {
                        visited.Add(neighbor.Value);
                        queue.Enqueue(neighbor);
                        nextLevelNodes.Add(neighbor.Value);
                    }
                }
            }

            if (nextLevelNodes.Count > 0)
            {
                currentLevel++;
                levels[currentLevel] = nextLevelNodes;
            }
        }

        return levels;
    }
}
