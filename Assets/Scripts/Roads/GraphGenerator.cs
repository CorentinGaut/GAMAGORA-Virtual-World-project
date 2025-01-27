using System.Collections.Generic;
using UnityEngine;
using Terrain;
using UnityEngine.Events;

namespace Roads
{
	public class GraphGenerator : MonoBehaviour
    {
        public Graph      graph;
        public UnityEvent onGraphFilled;

        [SerializeField] 
    	private GameObject terrainGenerator;
    	
    	[Header("Debug graph")]
    	[SerializeField] 
    	private int indPoint;
    	[SerializeField] 
    	private Color baseColor;

	    public Terrain.Terrain land;

        private void Awake()
        {
            onGraphFilled = new();            
        }

        private void Start()
    	{
		    land = terrainGenerator.GetComponent<TerrainManager>().TerrainGenerator;
		    
    		GetComponent<LineRenderer>().startColor = baseColor;
    		GetComponent<LineRenderer>().endColor = baseColor;
    		
    		//Get all the vertices of the map and remove all the vertices where position.y < 0.3 (Water)
    		graph = new Graph(land.data.Length);
    		for (int i = 0; i < land.data.Length; i++)
		    {
			    Vector3 v = land.IndexToWorld(i); 
			    graph.AddPoint(i, v);
    		}
    		
    		//Create all the edge for the graph for the direct neighbor
		    for (int i = 0; i < graph.Position.Length - 1; i++)
		    {
			    bool next = (i + 1) % land.NX != 0;
			    bool nextY = (i + land.NX) / land.NY != land.NY;
			    
			    if (graph.Position[i].y <= .3f)
				    continue;
			    
			    if (next && graph.Position[i + 1].y > .3f)
				    graph.AddEdge(new(i, i+1));
			    
			    if (nextY && graph.Position[i + land.NX].y > .3f)
				    graph.AddEdge(new(i, i + land.NX));
			    
			    if (next && nextY && graph.Position[i + 1 + land.NX].y > .3f)
				    graph.AddEdge(new(i, i + 1 + land.NX));
			    
			    if (nextY && i % land.NX != 0 && graph.Position[i - 1 + land.NX].y > .3f)
				    graph.AddEdge(new(i, i - 1 + land.NX));
		    }

            onGraphFilled.Invoke();
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
