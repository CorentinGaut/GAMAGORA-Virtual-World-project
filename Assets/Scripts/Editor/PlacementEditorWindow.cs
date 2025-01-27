using UnityEngine;
using UnityEditor;
using System;

using Random = UnityEngine.Random;
using UnityEditor.SceneManagement;

public class PlacementEditorWindow : EditorWindow
{
    private Texture2D noiseMapTexture;
    private float density = 0.5f;
    private GameObject prefab;
    private PlacementGenes genes;
    private GameObject terrain;

    private static string GenesSaveName
    {
        get { return $"Trees_{Application.productName}_{EditorSceneManager.GetActiveScene().name}"; }
    }

    [MenuItem("Tools/Trees/Tree Placement")]
    public static void ShowWindow()
    {
        GetWindow<PlacementEditorWindow>("Plant Placement");
    }

    private void OnEnable()
    {
        genes = PlacementGenes.Load(GenesSaveName);
    }

    private void OnDisable()
    {
        PlacementGenes.Save(GenesSaveName, genes);
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        noiseMapTexture = (Texture2D) EditorGUILayout.ObjectField("Noise Map Texture", noiseMapTexture, typeof(Texture2D), false);
        if(GUILayout.Button("Generate Noise"))
        {
            //int width = (int)terrain.GetComponent<Renderer>().bounds.size.x;
            //int height = (int)terrain.GetComponent<Renderer>().bounds.size.z;
            int width = (int)Terrain.activeTerrain.terrainData.size.x;
            int depth = (int)Terrain.activeTerrain.terrainData.size.z;

            float scale = 5;
            noiseMapTexture = Noise.GetNoiseMap(width, depth, scale);

        }
        EditorGUILayout.EndHorizontal();

        genes.maxHeight = EditorGUILayout.Slider("Max Height", genes.maxHeight, 0, 1000);
        genes.maxSteepness = EditorGUILayout.Slider("Max Steepness", genes.maxSteepness, 0, 90);

        density = EditorGUILayout.Slider("Density", density, 0, 1);

        prefab = (GameObject)EditorGUILayout.ObjectField("Object Prefab", prefab, typeof(GameObject), false);
    
        if(GUILayout.Button("Place Objects"))
        {
            PlaceObjects(Terrain.activeTerrain, noiseMapTexture, genes, prefab);
        }
    }

    public static void PlaceObjects(Terrain terrain, Texture2D noiseMapTexture, PlacementGenes genes, GameObject prefab)
    {
        Transform parent = new GameObject("PlaceObjects").transform;

        //terrain.GetComponent<Renderer>().bounds.size.x
        for (int x = 0; x < terrain.terrainData.size.x; x++)
        {
            for(int z = 0; z < terrain.terrainData.size.z; z++)
            {
                float noiseMapValue = noiseMapTexture.GetPixel(x, z).g;
                
                //If value is above threshold, instantiate a plant prefab at this location
                if(Fitness(terrain, noiseMapTexture, genes, x, z) > 1 - genes.density)
                {
                    Vector3 pos = new Vector3(x + Random.Range(-0.5f, 0.5f), 0, z + Random.Range(-0.5f, 0.5f));
                    pos.y = terrain.terrainData.GetInterpolatedHeight(x / terrain.terrainData.size.x, z / (float)terrain.terrainData.size.y);
                    
                    GameObject go = Instantiate(prefab, pos, Quaternion.identity);
                    go.transform.SetParent(parent);
                }
            }
        }
    }

    private static float Fitness(Terrain terrain, Texture2D noiseMapTexture, PlacementGenes genes, int x, int z)
    {
        float fitness = noiseMapTexture.GetPixel(x, z).g;

        fitness += Random.Range(-0.25f, 0.25f);
        
        float steepness = terrain.terrainData.GetSteepness(x / terrain.terrainData.size.x, z /  terrain.terrainData.size.z);
        if(steepness > genes.maxSteepness)
        {
            fitness -= 0.7f;
        }
        /*
        float height = terrain.terrainData.GetHeight(x, z);
        if (height > genes.maxHeight)
        {
            fitness -= 0.7f;
        }
        */
        return fitness;
    }

    [Serializable]
    public struct PlacementGenes
    {
        public float density;
        public float maxHeight;
        public float maxSteepness;

        //If already have a value, load it, else generate a default value
        internal static PlacementGenes Load(string saveName)
        {
            PlacementGenes genes;
            string saveData = EditorPrefs.GetString(saveName);

            if(string.IsNullOrEmpty(saveData))
            {
                genes = new PlacementGenes();
                genes.density = 0.5f;
                genes.maxHeight = 100;
                genes.maxSteepness = 25;
            }
            else
            {
                genes = JsonUtility.FromJson<PlacementGenes>(saveData);
            }

            return genes;
        }

        //Save json into the string
        internal static void Save(string saveName, PlacementGenes genes)
        {
            EditorPrefs.SetString(saveName, JsonUtility.ToJson(genes));
        }
    }

}
