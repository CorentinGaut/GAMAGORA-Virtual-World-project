using UnityEditor;
using UnityEngine;

namespace Graph
{
    [CustomEditor(typeof(GraphGenerator))]
    public class GraphGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
    
            GraphGenerator generator = (GraphGenerator) target;
            if (GUILayout.Button("Generate edge in line renderer"))
            {
                generator.GenerateLineRenderer();
            }
        }
    }
}
