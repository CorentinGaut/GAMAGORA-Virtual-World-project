using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Roads
{
	public class Graph
    {
    	public Vector3[] Position {get; set;}
    	public List<Edge> Edges { get; set; }

        public Graph(int size)
        {
            Position = new Vector3[size];
    		Edges = new List<Edge>();
    	}
    
    	public Graph(Vector3[] position, List<Edge> edges)
    	{
    		Position = position;
    		Edges = edges;
    	}

        public void AddPoint(int index, Vector3 point)
        {
            Position[index] = point;
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
