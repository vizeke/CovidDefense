using System.Collections.Generic;
using UnityEngine;

// Class definition to save the vertex and its edges.
public class Vertex
{
    // Name of the vertex.
    public string Name { get; set; }

    // Edges associated with the vertex
    public List<string> Edges { get; set; }

    public float Speed { get; set; }

    public Vector3 Point { get; set; }

    /**
     * Creates a new vertex with empty edges.
     * @param vName Name of the vertex
     */
    public Vertex(string name, Vector3 Point, float speed = 1.0f)
    {
        this.Name = name;
        this.Point = Point;
        this.Speed = speed;
        this.Edges = new List<string>();
    }
}