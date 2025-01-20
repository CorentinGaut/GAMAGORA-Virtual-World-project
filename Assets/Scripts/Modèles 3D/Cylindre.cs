using System;
using System. Collections;
using System. Collections. Generic;
using UnityEngine;


public class Cylindre
{
	// Attributs
	
	private double
		hauteur,
		rayon
	;
	private int nbMeridiens;
	
	
	// Constructeur
	
	public Cylindre (double h, double r, int n = 20)
	{
		this. hauteur = Math. Abs (h);
		this. rayon = Math. Abs (r);
		this. nbMeridiens = n;
		
		if (this. nbMeridiens < 3)
		{
			this. nbMeridiens = 3;
			Debug. LogWarning ("Attention : nombre de méridiens d'un cylindre inférieur à 3");
		}
	}
	
	
	// Méthode
	
	public void modeliser (Vector3 translation)
	{
		List <Vector3> sommets = new List <Vector3> ();
		List <int> triangles   = new List <int> ();
		
		// Calcul des sommets
		double angle;
		float x, y, z;
		y = Convert. ToSingle (this. hauteur);
		for (int meridien = 0; meridien < this. nbMeridiens; meridien ++)
		{
			angle = 2 * Math. PI * meridien / this. nbMeridiens;
			x = Convert. ToSingle (this. rayon * Math. Cos (angle));
			z = Convert. ToSingle (this. rayon * Math. Sin (angle));
			sommets. Add (new Vector3 (x, 0, z) + translation);
			sommets. Add (new Vector3 (x, y, z) + translation);
		}
		sommets. Add (translation);
		sommets. Add (new Vector3 (0, y, 0) + translation);
		
		// Calcul des rectangles
		for (int meridien = 0; meridien < this. nbMeridiens * 2 - 2; meridien += 2)
		{
			triangles. AddRange (new int []
			{
				meridien, meridien + 1, meridien + 2
			}
			);
			triangles. AddRange (new int []
			{
				meridien + 1, meridien + 3, meridien + 2
			}
			);
		}
		int appui  = 2 * this. nbMeridiens;
		int pointe = appui + 1;
		triangles. AddRange (new int []
		{
			0, appui - 2, appui - 1
		}
		);
		triangles. AddRange (new int []
		{
			0, appui - 1, 1
		}
		);
		
		// Calcul des deux disques
		for (int meridien = 0; meridien < appui - 2; meridien += 2)
		{
			triangles. AddRange (new int []
			{
				appui, meridien, meridien + 2
			}
			);
			triangles. AddRange (new int []
			{
				pointe, meridien + 3, meridien + 1
			}
			);
		}
		triangles. AddRange (new int []
		{
			0, appui, appui - 2
		}
		);
		triangles. AddRange (new int []
		{
			1, appui - 1, pointe
		}
		);
		
		Generation. generer ("cylindre", ref sommets, ref triangles);
	}
}
