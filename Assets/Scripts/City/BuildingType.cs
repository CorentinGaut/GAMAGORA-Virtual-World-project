using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSystem
{
    [Serializable]
    public class BuildingType
    {
        [SerializeField] private GameObject[] prefabs;
        public int sizeRequired;
        public int quantity;
        public int QuantityAlreadyPlaced;

        public GameObject GetPrefab()
        {
            QuantityAlreadyPlaced++;
            if(prefabs.Length > 1)
            {
                var random = UnityEngine.Random.Range(0, prefabs.Length);
                return prefabs[random];
            }
            return prefabs[0];
        }

        public bool IsBuildingAvailable()
        {
            return QuantityAlreadyPlaced < quantity;
        }

        public void Reset()
        {
            QuantityAlreadyPlaced = 0;
        }
    }
}
