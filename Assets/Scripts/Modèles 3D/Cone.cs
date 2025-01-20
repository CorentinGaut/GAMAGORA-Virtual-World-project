using System;
using System. Collections;
using System. Collections. Generic;
using UnityEngine;


public class Cone
{
	// Attributs
	
	private double
		rayon,
		hauteur,
		troncature
	;
	private int nbMeridiens;
	
	
	// Constructeur
	
	public Cone (double r, double h, double t, int n = 20)
	{
		this. rayon = Math. Abs (r);
		this. hauteur = Math. Abs (h);
		this. troncature = Math. Abs (t);
		this. nbMeridiens = n;
		
		if (this. troncature > this. hauteur)
		{
			this. troncature = 0;
			Debug. LogWarning ("Attention : troncature d'un cône plus grande que sa hauteur");
		}
		if (this. nbMeridiens < 3)
		{
			this. nbMeridiens = 3;
			Debug. LogWarning ("Attention : nombre de méridiens d'un cône inférieur à 3");
		}
	}
	
	
	// Méthode
	
	public void modeliser (Vector3 translation)
	{
		List <Vector3> sommets = new List <Vector3> ();
		List <int> triangles   = new List <int> ();
		
		if (this. troncature == 0)
		{
			
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
			}
			sommets. Add (translation);
			sommets. Add (new Vector3 (0, y, 0) + translation);

			// Calcul du disque et de la pente
			int appui  = this. nbMeridiens;
			int pointe = appui + 1;
			for (int meridien = 0; meridien < appui - 1; meridien ++)
			{
				triangles. AddRange (new int []
				{
					appui, meridien, meridien + 1
				}
				);
				triangles. AddRange (new int []
				{
					pointe, meridien + 1, meridien
				}
				);
			triangles. AddRange (new int []
			{
				0, appui, appui - 1
			}
			);
			}
			triangles. AddRange (new int []
			{
				0, appui - 1, pointe
			}
			);
			
		}
		
		else
		{
			
			// Calcul des sommets
			double angle;
			float coefReduction = Convert. ToSingle (this. troncature / this. hauteur);
			float x, y, z;
			y = Convert. ToSingle (this. hauteur - this. troncature);
			for (int meridien = 0; meridien < this. nbMeridiens; meridien ++)
			{
				angle = 2 * Math. PI * meridien / this. nbMeridiens;
				x = Convert. ToSingle (this. rayon * Math. Cos (angle));
				z = Convert. ToSingle (this. rayon * Math. Sin (angle));
				sommets. Add (new Vector3 (x, 0, z) + translation);
				x *= coefReduction;
				z *= coefReduction;
				sommets. Add (new Vector3 (x, y, z) + translation);
			}
			sommets. Add (translation);
			sommets. Add (new Vector3 (0, y, 0) + translation);
			
			// Calcul de la pente
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
			
		}
		
		Generation. generer ("cone", ref sommets, ref triangles);
	}
}
