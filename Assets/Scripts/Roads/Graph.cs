using System.Collections.Generic;
using UnityEngine;

namespace Graph
{
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
    
    	public List<int> GetNeighbor(int indPoint)
    	{
    		List<int> neighbors = new();
    		for (int i = 0; i < Edges.Count; i++)
    		{
    			if (Edges[i].From == indPoint)
    			{
    				neighbors.Add(Edges[i].To);	
    			}else if (Edges[i].To == indPoint)
    			{
    				neighbors.Add(Edges[i].From);
    			}
    		}
    		return neighbors;
    	}
    }
}
