using LSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Visualizer : MonoBehaviour
{
    public LSystemGenerator lsystem;
    List<Vector3> positions = new List<Vector3>();
    public GameObject prefab;
    public Material lineMaterial;

    private int length = 8;
    private int angle = 90;

    public int Length{
        get { if(length>0) { return length; } else { return 1; } }
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
                    savePoints.Push(new AgentParameter()
                    {
                        position = currentPosition,
                        direction = direction,
                        length = Length
                    });
                    break;
                case EncodingLetters.load:
                    if (savePoints.Count > 0)
                    {
                        var AgentParameter = savePoints.Pop();
                        currentPosition = AgentParameter.position;
                        direction = AgentParameter.direction;
                        Length = AgentParameter.length;
                    }
                    else
                    {
                        throw new System.Exception("No save point to load");
                    }
                    break;
                case EncodingLetters.forward:
                    tempPosition = currentPosition + direction * length;
                    DrawLine(tempPosition, currentPosition, Color.red);
                    Length += 2;
                    positions.Add(currentPosition);
                    break;
                case EncodingLetters.right:
                    direction = Quaternion.AngleAxis(angle, Vector3.up) * direction;
                    break;
                case EncodingLetters.left:
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

    public enum EncodingLetters 
    { 
        unknown = '1',
        save = '[',
        load = ']',
        forward = 'F',
        right = '+',
        left = '-'
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
