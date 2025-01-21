using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LSystem
{
    public class StructurHelper : MonoBehaviour
    {
        public GameObject prefab;
        public Dictionary<Vector3Int, GameObject> structuresDictionary = new Dictionary<Vector3Int, GameObject>();

        public void PlaceStructureAroundRoad(List<Vector3Int> roadPosition)
        {
            Dictionary<Vector3Int, Direction> freeEstateSpots = FindFreeSpacesAroundRoad(roadPosition);
            foreach (var position in freeEstateSpots.Keys)
            {
                Instantiate(prefab, position, Quaternion.identity, transform);
            }
        }

        private Dictionary<Vector3Int, Direction> FindFreeSpacesAroundRoad(List<Vector3Int> roadPosition)
        {
            Dictionary<Vector3Int, Direction> freeSpaces = new Dictionary<Vector3Int, Direction>();
            foreach (var position in roadPosition)
            {
                var neighbourDirections = PlacementHelper.FindNeighbour(position, roadPosition);
                foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                {
                    if (!neighbourDirections.Contains(direction))
                    {
                        var newPosition = position + PlacementHelper.GetOffsetFromDirection(direction);
                        if (freeSpaces.ContainsKey(newPosition))
                        {
                            continue;
                        }
                        freeSpaces.Add(newPosition, Direction.Right);
                    }
                }
            }
            return freeSpaces;
        }
    }
}
