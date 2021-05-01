using UnityEngine;

public class RoadData : MonoBehaviour
{
    public Graph Graph { get; set; }

    private void InitRoad(SnappingGrid grid)
    {
        Graph = new Graph();

        // Vertices
        Graph.AddVertex(new Vertex("a", grid.PlaceInGrid(5, 0)));
        Graph.AddVertex(new Vertex("b", grid.PlaceInGrid(10, 5)));
        Graph.AddVertex(new Vertex("c", grid.PlaceInGrid(10, 20)));
        Graph.AddVertex(new Vertex("d", grid.PlaceInGrid(3, 20)));
        Graph.AddVertex(new Vertex("e", grid.PlaceInGrid(3, 20)));

        // Edges
        Graph.AddAnEdge("a", "b");
        Graph.AddAnEdge("b", "c");
        Graph.AddAnEdge("c", "d");
    }
}
