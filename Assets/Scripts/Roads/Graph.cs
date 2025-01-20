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
		foreach (Edge e in Edges)
		{
			if ((e.From == edge.From && e.To == edge.To) || (e.To == edge.From && e.From == edge.To))
			{
				return;
			}
		}
		Edges.Add(edge);
	}

	public List<Vector3> GetNeighbor(Vector3 point)
	{
		List<Vector3> neighbors = new List<Vector3>();
		for (int i = 0; i < Edges.Count; i++)
		{
			if (Edges[i].From.x == point.x && Edges[i].From.y == point.y && Edges[i].From.z == point.z)
			{
				neighbors.Add(Edges[i].To);	
			}else if (Edges[i].To.x == point.x && Edges[i].To.y == point.y && Edges[i].To.z == point.z)
			{
				neighbors.Add(Edges[i].From);
			}
		}
		return neighbors;
	}
}