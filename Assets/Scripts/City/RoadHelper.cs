using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LSystem
{
    public class RoadHelper : MonoBehaviour
    {
        public GameObject roadStraight, roadCorner, road3way, road4way, roadEnd;
        private readonly Dictionary<Vector3Int, GameObject> roadDictionary = new();
        private readonly HashSet<Vector3Int> fixedRoadCandidates = new();

        public List<Vector3Int> GetRoadPosition()
        {
            return roadDictionary.Keys.ToList();
        }

        public void PlaceStreetPositions(Vector3 startPosition, Vector3Int direction, int length)
        {
            var rotation = Quaternion.identity;
            if (direction.x == 0)
            {
                rotation = Quaternion.Euler(0, 90, 0);
            }
            for (int i = 0; i < length; i++)
            {
                var position = Vector3Int.RoundToInt(startPosition + direction * i);
                if (roadDictionary.ContainsKey(position))
                {
                    continue;
                }
                var road = Instantiate(roadStraight, position, rotation, transform);
                roadDictionary.Add(position, road);
                if (i == 0 || i == length - 1)
                {
                    fixedRoadCandidates.Add(position);
                }
            }
        }

    }
}