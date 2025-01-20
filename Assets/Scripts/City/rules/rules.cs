using UnityEngine;

namespace LSystem {
    [CreateAssetMenu(fileName = "rules", menuName = "LSystem/Rule1")]
    public class rules : ScriptableObject
    {
        public string letter;
        [SerializeField] private string[] results = null;
        [SerializeField] private bool randomResult = false;

        public bool randomIngoreRuleModifier = true;
        [Range(0, 1)] public float chanceToIgnoreRule = 0.3f;

        public string GetResult()
        {
            if (randomResult)
            {
                int randomIndex = Random.Range(0, results.Length);
                return results[randomIndex];
            }
            return results[0];
        }
    }
}