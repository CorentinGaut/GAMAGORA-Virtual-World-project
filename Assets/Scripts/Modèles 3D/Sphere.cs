using System;
using System. Collections;
using System. Collections. Generic;
using UnityEngine;


public class Sphere
{
	// Attributs
	
	private double rayon;
	private int
		nbMeridiens,
		nbParalleles
	;
	
	
	// Constructeur
	
	public Sphere (double r, int nm = 20, int np = 9)
	{
		this. rayon = Math. Abs (r);
		this. nbMeridiens = nm;
		this. nbParalleles = np;
		
		if (this. nbMeridiens < 3)
		{
			this. nbMeridiens = 3;
			Debug. LogWarning ("Attention : nombre de méridiens d'une sphère inférieur à 3");
		}
		if (this. nbParalleles < 1)
		{
			this. nbParalleles = 1;
			Debug. LogWarning ("Attention : nombre de parallèles d'une sphère inférieur à 1");
		}
	}
	
	
	// Méthode
	
	public void modeliser (Vector3 translation)
	{
		List <Vector3> sommets = new List <Vector3> ();
		List <int> triangles   = new List <int> ();
		
		// Calcul des sommets
		double
			angleVertical,
			angleHorizontal,
			rayonParallele
		;
		float x, y, z;
		for (int parallele = 1; parallele <= this. nbParalleles; parallele ++)
		{
			angleVertical = Math. PI * parallele / (this. nbParalleles + 1);
			y              = Convert. ToSingle (this. rayon * Math. Cos (angleVertical));
			rayonParallele = Convert. ToSingle (this. rayon * Math. Sin (angleVertical));
			for (int meridien = 0; meridien < this. nbMeridiens; meridien ++)
			{
				angleHorizontal = 2 * Math. PI * meridien / this. nbMeridiens;
				x = Convert. ToSingle (rayonParallele * Math. Cos (angleHorizontal));
				z = Convert. ToSingle (rayonParallele * Math. Sin (angleHorizontal));
				sommets. Add (new Vector3 (x, y, z) + translation);
			}
		}
		float demiHauteur = Convert. ToSingle (this. rayon);
		sommets. Add (new Vector3 (0,  demiHauteur, 0) + translation);
		sommets. Add (new Vector3 (0, -demiHauteur, 0) + translation);
		
		// Calcul de la partie tiède du globe
		int decalage;
		for (int parallele = 0; parallele < this. nbParalleles - 1; parallele ++)
		{
			decalage = parallele * this. nbMeridiens;
			for (int meridien = 0; meridien < this. nbMeridiens - 1; meridien ++)
			{
				triangles. AddRange (new int []
				{
					meridien + decalage, meridien + decalage + 1, meridien + decalage + this. nbMeridiens + 1
				}
				);
				triangles. AddRange (new int []
				{
					meridien + decalage, meridien + decalage + this. nbMeridiens + 1, meridien + decalage + this. nbMeridiens
				}
				);
			}
			triangles. AddRange (new int []
			{
				decalage + this. nbMeridiens - 1, decalage, decalage + this. nbMeridiens
			}
			);
			triangles. AddRange (new int []
			{
				decalage + this. nbMeridiens - 1, decalage + this. nbMeridiens, decalage + 2 * this. nbMeridiens - 1
			}
			);
		}
		
		// Calcul des cercles polaires
		int poleNord = this. nbParalleles * this. nbMeridiens;
		int poleSud  = poleNord + 1;
		int decalageSud = poleNord - this. nbMeridiens;
		for (int meridien = 0; meridien < this. nbMeridiens - 1; meridien ++)
		{
			triangles. AddRange (new int []
			{
				poleNord, meridien + 1, meridien
			}
			);
			triangles. AddRange (new int []
			{
				poleSud, meridien + decalageSud, meridien + decalageSud + 1
			}
			);
		}
		triangles. AddRange (new int []
		{
			poleNord, 0, this. nbMeridiens - 1
		}
		);
		triangles. AddRange (new int []
		{
			poleSud, poleNord - 1, decalageSud
		}
		);
		
		Generation. generer ("sphere", ref sommets, ref triangles);
	}
}
