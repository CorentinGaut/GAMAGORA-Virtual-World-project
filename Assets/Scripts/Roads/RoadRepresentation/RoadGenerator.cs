using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public List<List<Vector3>> pointsList; // List of lists of points to link to create the roads
    public GameObject roadSegmentPrefab; // Prefab for the road segment 
    public int bezierResolution = 40; // Detail level of the roads (maybe too much)

    private GameObject _roadParent; // SO the roads are not all over the place

    void Start()
    {
        // !TODO!  Example  only change with the reak data from the astar
        pointsList = new List<List<Vector3>>
        {
            new List<Vector3>
            {
                new Vector3(0, 0, 0),
                new Vector3(5, 0, 3),
                new Vector3(10, 1, 0),
                new Vector3(15, 1, -3)
            },
            new List<Vector3>
            {
                new Vector3(0, 1, 0),
                new Vector3(4, 1, 2),
                new Vector3(8, 2, -1),
                new Vector3(12, 2, -4)
            }
        };

        // Hold all road segments
        _roadParent = new GameObject("GeneratedRoads");

        GenerateRoads();
    }

    public void GenerateRoads()
    {
        // Iterate through each sublist of points
        for (int listIndex = 0; listIndex < pointsList.Count; listIndex++)
        {
            List<Vector3> points = pointsList[listIndex];

            // Create a separate parent for each list (can be removed just clearer)
            GameObject subRoadParent = new GameObject($"RoadGroup_{listIndex}");
            subRoadParent.transform.SetParent(_roadParent.transform);

            for (int i = 0; i < points.Count - 1; i++)
            {
                Vector3 p0 = points[i];
                Vector3 p3 = points[i + 1];

                // Control points for Bezier curve 
                Vector3 p1 = p0 + (points[i + 1] - points[i]).normalized * 2.0f;
                Vector3 p2 = p3 - (points[i + 1] - points[i]).normalized * 2.0f;

                GenerateBezierRoad(p0, p1, p2, p3, subRoadParent); // Generate the road
            }
        }
    }

    void GenerateBezierRoad(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, GameObject parent)
    {
        Vector3 previousPoint = CalculateBezierPoint(0, p0, p1, p2, p3);  // maybe stock instead of calculating again 

        for (int j = 1; j <= bezierResolution; j++) // Start from 1 to avoid duplicate points
        {
            float t = (float)j / bezierResolution;

            // Calculate the current point on the Bezier curve
            Vector3 currentPoint = CalculateBezierPoint(t, p0, p1, p2, p3);

            // Create a road segment between the previous point and the current point
            CreateRoadSegment(previousPoint, currentPoint, parent);

            // Update the previous point
            previousPoint = currentPoint;
        }
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        return uuu * p0 + 3 * uu * t * p1 + 3 * u * tt * p2 + ttt * p3;
    }

    void CreateRoadSegment(Vector3 start, Vector3 end, GameObject parent)
    {
        // Calculate the position and rotation of the road segment
        Vector3 position = (start + end) / 2;
        Quaternion rotation = Quaternion.LookRotation(end - start);

        // Instantiate the road segment
        GameObject roadSegment = Instantiate(roadSegmentPrefab, position, rotation);

        // Scale the road segment to match the distance
        float distance = Vector3.Distance(start, end);
        roadSegment.transform.localScale = new Vector3(1, 0.2f, distance);  // Maybe change the parameters depending on the tests

        // Parent the road segment to the provided parent GameObject
        roadSegment.transform.SetParent(parent.transform);
    }
}
