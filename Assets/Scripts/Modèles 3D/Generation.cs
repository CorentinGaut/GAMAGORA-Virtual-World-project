using System;
using System. Collections;
using System. Collections. Generic;
using UnityEngine;


public class Generation: MonoBehaviour
{
    void Start ()
    {
		new Rectangle (4, 6). modeliser (Vector3. zero);
		new Cylindre (4, 2).  modeliser (new Vector3 (0, 0, -5));
		new Sphere (2).       modeliser (new Vector3 (0, 2, -11));
		new Cone (2, 4, 5).   modeliser (new Vector3 (0, -6, 4)); // Génère un warning
		new Cone (2, 4, 1).   modeliser (new Vector3 (0, -6, -2));
		new PacMan (2, 54).   modeliser (new Vector3 (0, -4, -11));
    }
	
	
	public static void generer (string nom, ref List <Vector3> sommets, ref List <int> triangles)
	{
        Mesh modele = new Mesh ();
		modele. vertices  = sommets. ToArray ();
		modele. triangles = triangles. ToArray ();
		
		GameObject objet = new GameObject (nom, typeof (MeshRenderer), typeof (MeshFilter));
		objet. GetComponent <MeshFilter> (). mesh = modele;
	}
}
