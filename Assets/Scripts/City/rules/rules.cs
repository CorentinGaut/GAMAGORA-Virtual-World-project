using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSystem {
    [CreateAssetMenu(fileName = "rules", menuName = "LSystem/Rule1")]
    public class rules : ScriptableObject
    {
        public string letter;
        [SerializeField] private string[] results = null;

        public string GetResult()
        {
            return results[0];
        }
    }
}