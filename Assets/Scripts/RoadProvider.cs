public static class RoadProvider
{
    public static Graph GetDefaultRoad(SnappingGrid grid)
    {
        var graph = new Graph();

        // Vertices
        graph.AddVertex(new Vertex("a", grid.PlaceInGrid(5, 0)));
        graph.AddVertex(new Vertex("b", grid.PlaceInGrid(5, 10)));
        graph.AddVertex(new Vertex("c", grid.PlaceInGrid(30, 10)));
        graph.AddVertex(new Vertex("d", grid.PlaceInGrid(30, 20)));
        graph.AddVertex(new Vertex("e", grid.PlaceInGrid(3, 20)));
        graph.AddVertex(new Vertex("f", grid.PlaceInGrid(3, 30)));

        // Edges
        graph.AddAnEdge("a", "b");
        graph.AddAnEdge("b", "c");
        graph.AddAnEdge("c", "d");
        graph.AddAnEdge("d", "e");
        graph.AddAnEdge("e", "f");

        return graph;
    }
}
