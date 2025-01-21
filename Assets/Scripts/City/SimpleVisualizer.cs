using LSystem;
using System.Collections.Generic;
using UnityEngine;

public class SimpleVisualizer : MonoBehaviour
{
    public LSystemGenerator lsystem;
    List<Vector3> positions = new List<Vector3>();
    public GameObject prefab;
    public Material lineMaterial;

    private int length = 8;
    private int angle = 90;

    public int Length{
        get 
        { 
            if(length > 0) 
            { 
                return length; 
            } 
            else 
            { 
                return 1; 
            } 
        }
        set => length = value;
    }
    
    private void Start()
    {
        var sequence = lsystem.GenerateSentence();
        VisualizeSequence(sequence);
    }

    private void VisualizeSequence(string sequence)
    {
        Stack<AgentParameter> savePoints = new Stack<AgentParameter>();
        var currentPosition = Vector3.zero;

        Vector3 direction = Vector3.forward;
        Vector3 tempPosition = Vector3.zero;

        positions.Add(currentPosition);

        foreach (var letter in sequence)
        {
            EncodingLetters encoding = (EncodingLetters)letter;
            switch (encoding)
            {
                case EncodingLetters.save: 
                    savePoints.Push(new AgentParameter
                    {
                        position = currentPosition,
                        direction = direction,
                        length = length
                    });
                    break;
                case EncodingLetters.load: 
                    if(savePoints.Count > 0)
                    {
                        var agentParameter = savePoints.Pop();
                        currentPosition = agentParameter.position;
                        direction = agentParameter.direction;
                        Length = agentParameter.length;
                    }
                    else
                    {
                        throw new System.Exception("Dont have save point in our stack");
                    }
                    break;
                case EncodingLetters.draw: 
                    tempPosition = currentPosition;
                    currentPosition += direction * length;
                    DrawLine(tempPosition, currentPosition, Color.red);
                    length -= 2;
                    positions.Add(currentPosition);
                    break;
                case EncodingLetters.turnRight: 
                    direction = Quaternion.AngleAxis(angle, Vector3.up) * direction;
                    break;
                case EncodingLetters.turnLeft:
                    direction = Quaternion.AngleAxis(-angle, Vector3.up) * direction;
                    break;
                default:
                    break;
            }
        }

        foreach (var pos in positions)
        {
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }

    private void DrawLine(Vector3 start, Vector3 end, Color c)
    {
        GameObject line = new GameObject("Line");
        line.transform.position = start;
        var lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startColor = c;
        lineRenderer.endColor = c;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}
