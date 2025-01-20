using System;
using System. Collections;
using System. Collections. Generic;
using UnityEngine;


public class Rectangle
{
	// Attributs
	
	private int
		vertical,
		horizontal
	;
	
	
	// Constructeur
	
	public Rectangle (int v = 1, int h = 1)
	{
		this. vertical = Math. Abs (v);
		this. horizontal = Math. Abs (h);
	}
	
	
	// Méthode
	
	public void modeliser (Vector3 translation)
	{
		List <Vector3> sommets = new List <Vector3> ();
		List <int> triangles   = new List <int> ();
		
		// Calcul des sommets
		for (int y = 0; y <= vertical; y ++)
		{
			for (int z = 0; z <= horizontal; z ++)
			{
				sommets. Add (new Vector3 (0, y, z) + translation);
			}
		}
		
		// Calcul des triangles
		int decalage;
		for (int y = 0; y < vertical; y ++)
		{
			for (int z = 0; z < horizontal; z ++)
			{
				decalage = y * (horizontal + 1) + z + 1;
				triangles. AddRange (new int []
				{
					decalage - 1, decalage, decalage + horizontal
				}
				);
				triangles. AddRange (new int []
				{
					decalage, decalage + horizontal + 1, decalage + horizontal
				}
				);
			}
		}
		
		Generation. generer ("rectangle", ref sommets, ref triangles);
	}
}
