using Terrain;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainManager))]
public class TerrainManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TerrainManager generator = (TerrainManager) target;
        if (GUILayout.Button("Generate"))
        {
            generator.GenerateTerrain();
            generator.UpdateModel();
        }
        if (GUILayout.Button("Update texture"))
        {
            generator.UpdateTexture();
        }
        if(GUILayout.Button("Erode"))
            generator.Erode();
    }

    private void OnInspectorUpdate() 
    {
        TerrainManager generator = (TerrainManager) target;
        if (generator.gain < 0f)
            generator.gain = 0f;
        if (generator.seed <= 0)
            generator.seed = 1;
        if (generator.octaves <= 0)
            generator.octaves = 1;
    }
    
}
