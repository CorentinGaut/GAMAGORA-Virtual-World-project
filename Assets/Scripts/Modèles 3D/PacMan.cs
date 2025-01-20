using System;
using System. Collections;
using System. Collections. Generic;
using UnityEngine;


public class PacMan
{
	// Attributs
	
	private double
		rayon,
		troncature
	;
	private int
		nbMeridiens,
		nbParalleles
	;
	
	
	// Constructeur
	
	public PacMan (double r, double t, int nm = 20, int np = 9)
	{
		this. rayon = Math. Abs (r);
		this. troncature = Math. Abs (t);
		this. nbMeridiens = nm;
		this. nbParalleles = np;
		
		if (this. troncature > 360)
		{
			this. troncature = 0;
			Debug. LogWarning ("Attention : troncature d'un Pac-Man plus grande que 360°");
		}
		if (this. nbMeridiens < 3)
		{
			this. nbMeridiens = 3;
			Debug. LogWarning ("Attention : nombre de méridiens d'un Pac-Man inférieur à 3");
		}
		if (this. nbParalleles < 1)
		{
			this. nbParalleles = 1;
			Debug. LogWarning ("Attention : nombre de parallèles d'un Pac-Man inférieur à 1");
		}
	}
	
	
	// Méthode
	
	public void modeliser (Vector3 translation)
	{
		List <Vector3> sommets = new List <Vector3> ();
		List <int> triangles   = new List <int> ();
		
		if (this. troncature == 0)
		{
			new Sphere (this. rayon). modeliser (translation);
		}
		
		else
		{
		
			// Calcul des sommets
			double
				angleVertical,
				angleHorizontal,
				rayonParallele
			;
			float x, y, z;
			this. troncature = 1 - this. troncature / 360;
			int meridienBordure = (int) Math. Ceiling (this. troncature * this. nbMeridiens) + 1;
			this. troncature *= 2 * Math. PI;
			for (int parallele = 1; parallele <= this. nbParalleles; parallele ++)
			{
				angleVertical = Math. PI * parallele / (this. nbParalleles + 1);
				y              = Convert. ToSingle (this. rayon * Math. Cos (angleVertical));
				rayonParallele = Convert. ToSingle (this. rayon * Math. Sin (angleVertical));
				for (int meridien = 0; meridien < meridienBordure - 1; meridien ++)
				{
					angleHorizontal = 2 * Math. PI * meridien / this. nbMeridiens;
					x = Convert. ToSingle (rayonParallele * Math. Cos (angleHorizontal));
					z = Convert. ToSingle (rayonParallele * Math. Sin (angleHorizontal));
					sommets. Add (new Vector3 (x, y, z) + translation);
				}
				x = Convert. ToSingle (rayonParallele * Math. Cos (this. troncature));
				z = Convert. ToSingle (rayonParallele * Math. Sin (this. troncature));
				sommets. Add (new Vector3 (x, y, z) + translation);
			}
			float demiHauteur = Convert. ToSingle (this. rayon);
			sommets. Add (new Vector3 (0,  demiHauteur, 0) + translation);
			sommets. Add (new Vector3 (0, -demiHauteur, 0) + translation);
			sommets. Add (translation);
			
			// Calcul de la partie tiède du globe
			int decalage;
			for (int parallele = 0; parallele < this. nbParalleles - 1; parallele ++)
			{
				decalage = parallele * meridienBordure;
				for (int meridien = 0; meridien < meridienBordure - 1; meridien ++)
				{
					triangles. AddRange (new int []
					{
						meridien + decalage, meridien + decalage + 1, meridien + decalage + meridienBordure + 1
					}
					);
					triangles. AddRange (new int []
					{
						meridien + decalage, meridien + decalage + meridienBordure + 1, meridien + decalage + meridienBordure
					}
					);
				}
			}
			
			// Calcul des cercles polaires
			int poleNord = this. nbParalleles * meridienBordure;
			int poleSud  = poleNord + 1;
			int decalageSud = poleNord - meridienBordure;
			for (int meridien = 0; meridien < meridienBordure - 1; meridien ++)
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
			
			// Calcul du noyau
			int centre = poleNord + 2;
			for (int parallele = 0; parallele < this. nbParalleles - 1; parallele ++)
			{
				decalage = parallele * meridienBordure;
				triangles. AddRange (new int []
				{
					centre, decalage, decalage + meridienBordure
				}
				);
				triangles. AddRange (new int []
				{
					centre, decalage + 2 * meridienBordure - 1, decalage + meridienBordure - 1
				}
				);
			}
			triangles. AddRange (new int []
			{
				centre, poleNord, 0
			}
			);
			triangles. AddRange (new int []
			{
				centre, meridienBordure - 1, poleNord
			}
			);
			triangles. AddRange (new int []
			{
				centre, decalageSud, poleSud
			}
			);
			triangles. AddRange (new int []
			{
				centre, poleSud, decalageSud + meridienBordure - 1
			}
			);
			
			Generation. generer ("pacMan", ref sommets, ref triangles);
		
		}
	}
}
