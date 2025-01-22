using System;
using System.Text;
using UnityEngine;


namespace LSystem {
    public class LSystemGenerator : MonoBehaviour
    {
        public Rule[] rules;

        public string rootSentence;
        [Range(0,100)] public int iterationLimit = 1;

        public bool randomIgnoreRuleModifier = true;
        [Range(0, 1)] public float chanceToIgnoreRule = 0.3f;

        private void Start()
        {
            Debug.Log(GenerateSentence());
        }

        public string GenerateSentence(string word = null)
        {
            if(word == null)
            {
                word = rootSentence;
            }
            return GrowRecursive(word);
        }

        //public static string GenerateRandomSentence(int length)
        //{
        //    char[] symbols = { '[', ']', 'F', '+', '-' };
        //    System.Text.StringBuilder sentence = new System.Text.StringBuilder();

        //    // Génération aléatoire des caractères
        //    for (int i = 0; i < length; i++)
        //    {
        //        char randomSymbol = symbols[UnityEngine.Random.Range(0, symbols.Length)];
        //        sentence.Append(randomSymbol);
        //    }

        //    return sentence.ToString();
        //}

        private string GrowRecursive(string word, int iterationIndex = 0) {
            if (iterationIndex >= iterationLimit)
            {
                return word;
            }
            StringBuilder newWord = new StringBuilder();

            foreach (char letter in word)
            {
                newWord.Append(letter);
                ProcessRulesRecursivelly(newWord, letter, iterationIndex);
            }

            return newWord.ToString();
        }

        private void ProcessRulesRecursivelly(StringBuilder newWord, char letter, int iterationIndex)
        {
            foreach (var rule in rules) { 
                if(rule.letter == letter.ToString())
                {
                    if (randomIgnoreRuleModifier && iterationIndex > 1)
                    {
                        if (UnityEngine.Random.value < chanceToIgnoreRule)
                        {
                            return;
                        }
                    }
                    newWord.Append(GrowRecursive(rule.GetResult(),iterationIndex+1));
                    
                }
            }
        }
    }
}








































//caca