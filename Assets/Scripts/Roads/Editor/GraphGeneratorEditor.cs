using UnityEditor;
using UnityEngine;

namespace Roads 
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

    //[CustomEditor(typeof(RoadGenerator))]
    //public class RoadGeneratorEditor : Editor
    //{
    //    public override void OnInspectorGUI()
    //    {
    //        DrawDefaultInspector();

    //        RoadGenerator generator = (RoadGenerator)target;
    //        if (GUILayout.Button("Generate edge in line renderer"))
    //        {
    //            generator.GenerateLineRenderer();
    //        }
    //    }
    //}
}
