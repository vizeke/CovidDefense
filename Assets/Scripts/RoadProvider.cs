using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadProvider : MonoBehaviour
{
    public static Graph GetDefaultRoad(SnappingGrid grid)
    {
        var graph = new Graph();

        // Vertices
        graph.AddVertex(new Vertex("a", grid.PlaceInGrid(5, 0)));
        graph.AddVertex(new Vertex("b", grid.PlaceInGrid(5, 10)));
        graph.AddVertex(new Vertex("c", grid.PlaceInGrid(27, 10)));
        graph.AddVertex(new Vertex("d", grid.PlaceInGrid(27, 20)));
        graph.AddVertex(new Vertex("e", grid.PlaceInGrid(3, 20)));
        graph.AddVertex(new Vertex("f", grid.PlaceInGrid(3, 29)));

        // Edges
        graph.AddAnEdge("a", "b");
        graph.AddAnEdge("b", "c");
        graph.AddAnEdge("c", "d");
        graph.AddAnEdge("d", "e");
        graph.AddAnEdge("e", "f");

        return graph;
    }

    public static List<GameObject> GetRoadBlocks(Graph road)
    {
        var listRoadBlocks = new List<GameObject>();

        var source = road.Source;
        var destination = road.AdjacencyList.FirstOrDefault(e => e.Name == source.Edges.First());
        var rodaBlockResource = Resources.Load<GameObject>($"Prefabs/roadBlock");

        while (destination != null)
        {
            var roadBlock = Instantiate(rodaBlockResource, source.Point, Quaternion.identity);

            var middle = LerpByDistance(source.Point, destination.Point, 0.5f);

            roadBlock.transform.position = middle;

            roadBlock.transform.localScale = ScaleByDistance(source.Point, destination.Point);

            listRoadBlocks.Add(roadBlock);

            source = destination;
            destination = road.AdjacencyList.FirstOrDefault(e => e.Name == source.Edges.FirstOrDefault());
        }

        return listRoadBlocks;
    }

    public static Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
    {
        return A + (B - A) * x;
    }

    public static Vector3 ScaleByDistance(Vector3 A, Vector3 B)
    {
        var vectorResult = B - A;

        return new Vector3(vectorResult.x == 0 ? 2 : vectorResult.x, 1, vectorResult.z == 0 ? 2 : vectorResult.z);
    }
}
