using System.Collections.Generic;
using UnityEngine;
using Terrain;

namespace Graph
{
	public class GraphGenerator : MonoBehaviour
    {
    	public Graph graph;
    	
    	[SerializeField] 
    	private GameObject terrainGenerator;
    	
    	[Header("Debug graph")]
    	[SerializeField] 
    	private int indPoint;
    	[SerializeField] 
    	private Color baseColor;

	    private Terrain.Terrain land;
    	
    	
    	private void Start()
    	{
		    land = terrainGenerator.GetComponent<TerrainManager>().TerrainGenerator;
		    
    		GetComponent<LineRenderer>().startColor = baseColor;
    		GetComponent<LineRenderer>().endColor = baseColor;
    		
    		//Get all the vertices of the map and remove all the vertices where position.y < 0.3 (Water)
    		graph = new Graph();
    		Debug.Log("Nb de points : " + land.data.Length);
    		for (int i = 0; i < land.data.Length; i++)
		    {
			    Vector3 v = land.IndexToWorld(i); 
			    
			    Debug.Log(v);
			    graph.AddPoint(v);
    		}
    		Debug.Log("Nb de points : " + graph.Position.Count);
    		
    		//Create all the edge for the graph for the direct neighbor
		    for (int i = 0; i < graph.Position.Count - 1; i++)
		    {
			    if (graph.Position[i].y <= .3f)
				    continue;
			    
			    if ((i + 1) % land.NX != 0 && graph.Position[i + 1].y > .3f)
				    graph.AddEdge(new(i, i+1));
			    
			    if ((i + land.NX) / land.NY != land.NY && graph.Position[i + land.NX].y > .3f)
				    graph.AddEdge(new(i, i + land.NX));
			    
			    if ((i + 1) % land.NX != 0 && (i + land.NX) / land.NY != land.NY && graph.Position[i + 1 + land.NX].y > .3f)
				    graph.AddEdge(new(i, i + 1 + land.NX));
			    
			    if ((i + land.NX) / land.NY != land.NY && i % land.NX != 0 && graph.Position[i - 1 + land.NX].y > .3f)
				    graph.AddEdge(new(i, i - 1 + land.NX));
		    }
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
}
