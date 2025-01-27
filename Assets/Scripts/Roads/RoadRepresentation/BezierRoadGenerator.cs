using System.Collections.Generic;
using UnityEngine;

namespace Roads
{
public class BezierRoadGenerator : MonoBehaviour
{
    [SerializeField]
    private GraphGenerator _gen;

    private RoadTracer _tracer;

    public List<Vector3> pointsList;

    [Header("Road Settings")]
    public float roadWidth = 1.0f; // Width of the road
    public int resolution = 20;   // Resolution for B�zier --> probably need to adjust depending on time spent generationg the roads

    [Header("Material Settings")]
    public Material roadMaterial; // Material for the road

    private GameObject parentRoadObject; // Parent for all road segments

    
    private void Start()
    {
        _gen.onGraphFilled.AddListener(OnGraphFilled);
        parentRoadObject = new GameObject("Roads"); // for a cleaner hieracrhy
    }

    private void OnDestroy()
    {
        if (_gen != null)
        {
            _gen.onGraphFilled.RemoveListener(OnGraphFilled);
        }
    }

    private void OnGraphFilled()
    {
        _tracer = new RoadTracer(_gen.graph, _gen.land);

        pointsList = _tracer.Trace(250, 7455);
        
        GenerateRoads();
    }

    /*void Start()
    {
        parentRoadObject = new GameObject("Roads"); // for a cleaner hieracrhy

        // Example !!! final version will need to be called or to call the place where the points lists are
        pointsList = new List<List<Vector3>>
        {
            new List<Vector3>
            {
                new Vector3(0, 0, 0),
                new Vector3(5, 0, 3),
                new Vector3(10, 1, 0),
                new Vector3(15, 5, -3)
            },
            new List<Vector3>
            {
                new Vector3(15, 1, -3),
                new Vector3(20, 1, 0),
                new Vector3(25, 2, 3),
                new Vector3(30, 2, 0)
            }
        };

        GenerateRoads();
        //
    }*/

    void GenerateRoads()
    {

        for (int i = 1; i < pointsList.Count; i += 2)
        {
            if (pointsList.Count - i > 4)
            {
                GenerateRoadMesh(pointsList[i - 1], pointsList[i], pointsList[i + 1], pointsList[i + 2]);
            }
        }
    }

    void GenerateRoadMesh(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i <= resolution; i++)
        {
            float t = i / (float)resolution;

            // Calculate the current point on the B�zier curve
            Vector3 centerPoint = CalculateBezierPoint(t, p0, p1, p2, p3);

            // Calculate the direction and perpendicular vector
            Vector3 forwardDirection = (CalculateBezierPoint(Mathf.Clamp01(t + 0.01f), p0, p1, p2, p3) - centerPoint).normalized;
            Vector3 perpendicular = Vector3.Cross(forwardDirection, Vector3.up) * roadWidth / 2.0f;

            Vector3 leftPoint = centerPoint - perpendicular;
            Vector3 rightPoint = centerPoint + perpendicular;

            // Add vertices
            vertices.Add(leftPoint);
            vertices.Add(rightPoint);

            // Add UV coordinates
            float vCoord = t;
            uvs.Add(new Vector2(0, vCoord)); // Left side of the road   I think/hope
            uvs.Add(new Vector2(1, vCoord)); // Right side of the road  I think/hope

            // Add triangles
            if (i > 0)
            {
                int baseIndex = (i - 1) * 2;

                // First triangle
                triangles.Add(baseIndex);
                triangles.Add(baseIndex + 1);
                triangles.Add(baseIndex + 2);

                // Second triangle
                triangles.Add(baseIndex + 1);
                triangles.Add(baseIndex + 3);
                triangles.Add(baseIndex + 2);
            }
        }

        // Create the road mesh
        Mesh roadMesh = new Mesh();
        roadMesh.vertices = vertices.ToArray();
        roadMesh.triangles = triangles.ToArray();
        roadMesh.uv = uvs.ToArray();

        roadMesh.RecalculateNormals();

        // Create a GameObject to hold the road mesh
        GameObject roadObject = new GameObject("RoadSegment", typeof(MeshFilter), typeof(MeshRenderer));
        roadObject.GetComponent<MeshFilter>().mesh = roadMesh;

        // Apply the material
        MeshRenderer renderer = roadObject.GetComponent<MeshRenderer>();        
        renderer.material = roadMaterial;


        roadObject.transform.parent = parentRoadObject.transform;
    }

        // Equation for B�zier curve using Bernstein
        private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        // Bernstein polynomials
        Vector3 point = uuu * p0; // (1-t)^3 * P0
        point += 3 * uu * t * p1; // 3(1-t)^2 * t * P1
        point += 3 * u * tt * p2; // 3(1-t) * t^2 * P2
        point += ttt * p3;        // t^3 * P3

        return point;
    }
}
}
