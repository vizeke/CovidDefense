using System.Collections.Generic;
using System.Linq;

public class Graph
{
    // An adjacency list to hold our graph data
    public List<Vertex> AdjacencyList { get; set; }

    public Vertex Source { get; set; }

    public Graph()
    {
        this.AdjacencyList = new List<Vertex>();
    }

    /**
    * A method to add a new vertex to the graph.
    * @param newVertex Name of the vertex to be added to the graph
    */
    public void AddVertex(Vertex newVertex)
    {
        // We will keep the implementation simple and focus on the concepts

        // If the vertex already exists, do nothing.
        if (this.AdjacencyList.Any(e => e.Name == newVertex.Name))
        {
            return;
        }

        if (this.AdjacencyList.Count() == 0)
        {
            this.Source = newVertex;
        }

        this.AdjacencyList.Add(newVertex);
    }

    /**
    * Adds an edge to the graph.
    * @param vertex1 One of the vertices between an edge
    * @param vertex2 Another vertex of an edge
    */
    public void AddAnEdge(string source, string destination)
    {
        // We will keep the implementation simple and focus on the concepts
        // Do not worry about handling invalid indexes or any other error cases.
        // We will assume all vertices are valid and already exist.

        // Add an vertex2 to vertex1 edges.
        this.AdjacencyList.Find(e => e.Name == source).Edges.Add(destination);

        // Add an vertex1 to vertex2 edges.
        // this.AdjList.Find(e => e.Name == origin).Edges.Add(destination);

        var vertexDestination = this.AdjacencyList.Find(e => e.Name == destination);
    }

    /**
    * Removes an edge between two vertices.
    * @param vertex1 One of the vertex of an edge to be removed
    * @param vertex2 ANother vertex of an edge to be removed
    */
    public void RemoveAnEdge(string source, string destination)
    {
        // We will keep the implementation simple and focus on the concepts
        // Do not worry about handling invalid indexes or any other error cases.
        // We will assume all vertices are valid and already exist.

        // Remove vertex2 from the edges of vertex1
        this.AdjacencyList.First(e => e.Name == source).Edges.Remove(destination);

        // Remove vertex1 from the edges of vertex2
        // this.AdjList.First(e => e.Name == origin).Edges.Remove(destination);
    }
}