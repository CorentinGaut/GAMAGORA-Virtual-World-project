using System;
using System. Collections;
using System. Collections. Generic;


class Langage
{
	private static readonly char [] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ". ToCharArray ();
	
	private Dictionary <char, string> grammaire = new Dictionary <char, string> ();
	private string mot;
	
	
	public Langage (string [] regles, string axiome = "")
	{
		for (int indice = 0; indice < regles. Length; indice ++)
		{
			string resultat = regles [indice];
			estMot (resultat);
			this. grammaire. Add (alphabet [indice], resultat);
		}
		
		this. changerMot (axiome);
	}
	
	
	public override string ToString ()
	{
		return this. mot;
	}
	
	
	public void changerMot (string axiome)
	{
		estMot (axiome);
		this. mot = axiome;
	}
	
	
	public void iterer (int nbIterations = 1)
	{
		for (int _i = 0; _i < nbIterations; _i ++)
		{
			this. iterer ();
		}
	}
	
	
	public void iterer ()
	{
		string nouveau = "";
		foreach (char lettre in this. mot)
		{
			if (this. grammaire. ContainsKey (lettre))
			{
				nouveau += this. grammaire [lettre];
			}
			else
			{
				nouveau += lettre;
			}
		}
		this. mot = nouveau;
	}
	
	
	private static bool estMot (string mot)
	{
		foreach (char lettre in mot)
		{
			if (Array. IndexOf (alphabet, Char. ToUpper (lettre)) == -1)
			{
				string messageErreur = "Le caractère " + lettre + " n'appartient pas à l'alphabet";
				Console. WriteLine (messageErreur);
				/*
				 *  Erreur Unity :
				 *  --------------
				 *  Debug. LogError (messageErreur);
				 *  UnityEditor. EditorApplication. isPlaying = false;
				 *  Application. Quit ();
				 */
				
				return false;
			}
		}
		
		return true;
	}
}


class Tests
{
	public static void Main (string[] args)
	{
		var chene1 = new Langage (new String [] {"B", "ACA", "C"});
		chene1. changerMot ("AA");
		chene1. iterer (5);
		Console. WriteLine (chene1);
		chene1. iterer ();
		Console. WriteLine (chene1);
		chene1. changerMot ("AABBCACCBABCCBACBCAB");
		chene1. iterer (5);
		Console. WriteLine (chene1);
		
		var chene2 = new Langage (new String [] {"B", "ACA"}, "AA");
		chene2. iterer (10);
		Console. WriteLine (chene2);
		
		var chene3 = new Langage (new String [] {"B", "ACA"}, "");
		chene3. iterer (10);
		Console. WriteLine (chene3);
		
		var chene4 = new Langage (new String [] {"B", ""}, "AA");
		chene4. iterer (10);
		Console. WriteLine (chene4);
		
		var chene5 = new Langage (new String [] {"B", "ACA", "C"}, "A5A");
		
		var chene6 = new Langage (new String [] {"B", "AC6A", "C"}, "AA");
	}
}
