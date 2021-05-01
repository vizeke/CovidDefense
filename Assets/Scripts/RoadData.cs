using UnityEngine;

public class RoadData : MonoBehaviour
{
    public Graph Graph { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Graph = new Graph();

        // Vertices
        Graph.AddVertex(new Vertex("a", new Vector3(0, 0, 0)));
        Graph.AddVertex(new Vertex("b", new Vector3(0, 0, 5)));
        Graph.AddVertex(new Vertex("c", new Vector3(5, 0, 5)));
        Graph.AddVertex(new Vertex("d", new Vector3(5, 0, 10)));

        // Edges
        Graph.AddAnEdge("a", "b");
        Graph.AddAnEdge("b", "c");
        Graph.AddAnEdge("c", "d");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
