using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.VersionControl;
using UnityEngine;

public class GraphGenerator : MonoBehaviour
{
	public Graph graph;
	
	[SerializeField] 
	private GameObject land;
	
	[Header("Debug graph")]
	[SerializeField] 
	private int indPoint;
	[SerializeField] 
	private Color baseColor;
	
	
	private void Start()
	{
		GetComponent<LineRenderer>().startColor = baseColor;
		GetComponent<LineRenderer>().endColor = baseColor;
		
		//Get all the vertices of the map and remove all the vertices where position.y < 0.3 (Water)
		graph = new Graph();
		Debug.Log("Nb de points : " + land.GetComponent<MeshFilter>().mesh.vertices.Length);
		foreach (Vector3 v in land.GetComponent<MeshFilter>().mesh.vertices)
		{
			// if (v.y >= .3f)
			// {
				Debug.Log(v);
				graph.AddPoint(v);
			// }
		}
		Debug.Log("Nb de points : " + graph.Position.Count);
		
		//Create all the edge for the graph for the direct neighbor
		int[] triangle = land.GetComponent<MeshFilter>().mesh.triangles;
		for (int i = 0; i < triangle.Length; i+=3)
		{
			int ind1 = triangle[i];
			int ind2 = triangle[i + 1];
			int ind3 = triangle[i + 2];
			
			Vector3 v1 = land.GetComponent<MeshFilter>().mesh.vertices[ind1];
			Vector3 v2 = land.GetComponent<MeshFilter>().mesh.vertices[ind2];
			Vector3 v3 = land.GetComponent<MeshFilter>().mesh.vertices[ind3];

			if (v1.y >= .3f && v2.y >= .3f)
				graph.AddEdge(new Edge(ind1, ind2));
			if (v1.y >= .3f && v3.y >= .3f)
				graph.AddEdge(new Edge(ind1, ind3));
			if (v3.y >= .3f && v2.y >= .3f)
				graph.AddEdge(new Edge(ind3, ind2));
		}

		//Create the edge for the point without triangles
		
		
	}

	public void GenerateLineRenderer()
	{
		List<int> neighbors = graph.GetNeighbor(indPoint);

		GetComponent<LineRenderer>().positionCount = neighbors.Count * 2;
		for (int i = 0; i < neighbors.Count; i++)
		{
			GetComponent<LineRenderer>().SetPosition(i*2, graph.Position[indPoint]);
			GetComponent<LineRenderer>().SetPosition(i*2 + 1, graph.Position[neighbors[i]]);
		}
	}
}