using System.Collections.Generic;
using UnityEngine;

public class Graph
{
	public List<Vector3> Position {get; set;}
	public List<Edge> Edges {get; set;}

	public Graph()
	{
		Position = new List<Vector3>();
		Edges = new List<Edge>();
	}

	public Graph(List<Vector3> position, List<Edge> edges)
	{
		Position = position;
		Edges = edges;
	}
	
	public void AddPoint(Vector3 point)
	{
		Position.Add(point);
	}

	public void AddEdge(Edge edge)
	{
		Edges.Add(edge);
	}
}