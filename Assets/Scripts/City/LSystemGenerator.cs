using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;


namespace LSystem {
    public class LSystemGenerator : MonoBehaviour
    {
        public rules[] rules;

        public string rootSentence;
        [Range(0,10)] public int iterationLimit = 1;

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
                    newWord.Append(GrowRecursive(rule.GetResult(),iterationIndex+1));
                    
                }
            }
        }
    }
}








































//caca