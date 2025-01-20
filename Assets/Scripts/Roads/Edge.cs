using UnityEngine;

public class Edge
{
	public Vector3 From { get; set; }
	public Vector3 To { get; set; }

	public Edge(Vector3 from, Vector3 to)
	{
		From = from;
		To = to;
	}
}