using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace LSystem
{
    public class RoadManager : MonoBehaviour
    {
        [SerializeField]
        GameObject[] roads;

        public RoadHelper roadHelper;

        Dictionary<Vector2Int, GameObject> roadMap;

        public void SetUp()
        {
            roadMap = new Dictionary<Vector2Int, GameObject>();
            roads = GameObject.FindGameObjectsWithTag("LSRoad");
            foreach (var road in roads)
            {
                if (road != null)
                {
                    Vector2Int pos = new Vector2Int((int)road.transform.position.x, (int)road.transform.position.z);
                    roadMap[pos] = road;
                }
            }
            RecheckRoad();
        }

        void RecheckRoad()
        {
            foreach (var road in roadMap)
            {
                Vector2Int right = new Vector2Int(road.Key.x + 1, road.Key.y);
                Vector2Int left = new Vector2Int(road.Key.x - 1, road.Key.y);
                Vector2Int front = new Vector2Int(road.Key.x, road.Key.y + 1);
                Vector2Int back = new Vector2Int(road.Key.x, road.Key.y - 1);

                int nbTrue = (CheckRoadExist(right) ? 1 : 0) + (CheckRoadExist(left) ? 1 : 0) + (CheckRoadExist(front) ? 1 : 0) + (CheckRoadExist(back) ? 1 : 0);

                Quaternion rotation = Quaternion.identity;
                Vector3 pos = new Vector3(road.Key.x, 0, road.Key.y);

                if (nbTrue == 1)
                {
                    Destroy(road.Value);
                    if (CheckRoadExist(back))
                    {
                        rotation = Quaternion.Euler(0, 90, 0);
                    }
                    else if (CheckRoadExist(left))
                    {
                        rotation = Quaternion.Euler(0, 180, 0);
                    }
                    else if (CheckRoadExist(front))
                    {
                        rotation = Quaternion.Euler(0, -90, 0);
                    }
                    Instantiate(roadHelper.roadEnd, pos, rotation, transform);
                }
                else if (nbTrue == 4)
                {
                    Destroy(road.Value);
                    Instantiate(roadHelper.road4way, pos, rotation, transform);
                }
                else if (nbTrue == 3)
                {
                    Destroy(road.Value);
                    if (CheckRoadExist(right) && CheckRoadExist(back) && CheckRoadExist(left))
                    {
                        rotation = Quaternion.Euler(0, 90, 0);
                    }
                    else if (CheckRoadExist(back) && CheckRoadExist(left) && CheckRoadExist(front))
                    {
                        rotation = Quaternion.Euler(0, 180, 0);
                    }
                    else if (CheckRoadExist(left) && CheckRoadExist(front) && CheckRoadExist(right))
                    {
                        rotation = Quaternion.Euler(0, -90, 0);
                    }
                    Instantiate(roadHelper.road3way, pos, rotation, transform);
                }
                else if (nbTrue == 2)
                {
                    Destroy(road.Value);
                    if(CheckRoadExist(left) && CheckRoadExist(right))
                    {
                        rotation = Quaternion.Euler(0, 0, 0);
                        Instantiate(roadHelper.roadStraight, pos, rotation, transform);
                    }
                    else if (CheckRoadExist(front) && CheckRoadExist(back))
                    {
                        rotation = Quaternion.Euler(0, 90, 0);
                        Instantiate(roadHelper.roadStraight, pos, rotation, transform);
                    }
                    else
                    {
                        if (CheckRoadExist(front) && CheckRoadExist(right))
                        {
                            rotation = Quaternion.Euler(0, 90, 0);
                        }
                        else if (CheckRoadExist(right) && CheckRoadExist(back))
                        {
                            rotation = Quaternion.Euler(0, 180, 0);
                        }
                        else if (CheckRoadExist(back) && CheckRoadExist(left))
                        {
                            rotation = Quaternion.Euler(0, -90, 0);
                        }
                        Instantiate(roadHelper.roadCorner, pos, rotation, transform);
                    }
                }
            }
        }

        bool CheckRoadExist(Vector2Int key)
        {
            return roadMap.ContainsKey(key);
        }

    }
}
