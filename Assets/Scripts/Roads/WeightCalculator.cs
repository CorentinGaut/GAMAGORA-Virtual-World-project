/*
 * Probably refactor those to include them in the Graph class
 *
 * Source : https://onlinelibrary.wiley.com/doi/full/10.1111/j.1467-8659.2009.01612.x
 */

using System;
using UnityEngine;

namespace Roads
{
    public class WeightCalculator
    {
        private const float _slopeLimit = 0.05f;
        private const float _averageSlopeLimit = 0.05f;
        private const float _curveLimit = Mathf.PI;

        private Graph _graph;
        private Terrain.Terrain _land;

        public WeightCalculator(Graph graph, Terrain.Terrain land)
        {
            _graph = graph;
            _land = land;
        }

        /*
         * Characteristic functions
         */

        // Calculate slope between two points, starting from p1 to p2
        // So if p1 is above p2 slope is negative and vice versa
        public float Slope(int i1, int i2)
        {
            var p1 = _graph.Position[i1];
            var p2 = _graph.Position[i2];

            int x = i2 % _land.NX; // Column
            int z = (int)((float)i2 / (float)_land.NX);
            if (x > 0 && x < _land.NX && z > 0 && z < _land.NY)
            {
                float avg = _land.GetSlope(x, z);
                if (avg > _averageSlopeLimit)
                {
                    return Mathf.Infinity;
                }
            }
            else
            {
                return Mathf.Infinity;
            }

            return Mathf.Abs((p2.y - p1.y) / (p2.x - p1.x));
        }

        // Calculate the angle formed by p1, p2 and p3
        public float Curvature(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            Vector3 a = new(p1.x - p2.x, p1.y - p2.y, p1.z - p2.z);
            Vector3 b = new(p1.x - p3.x, p1.y - p3.y, p1.z - p3.z);
            return Mathf.Acos(Vector3.Dot(a, b) / (a.magnitude * b.magnitude));
        }
        
        /*
         * Transfer functions
         */
        
        //public static float WhTransfer(float wh)
        //{
        //    return 0f;
        //}

        public float STransfer(float s)
        {
            if (s > _slopeLimit)
                return Mathf.Infinity;
            else
                return s;
        }

        public float CTransfer(float c)
        {
            var f = Mathf.Abs(c);
            if (f > _curveLimit)
                return Mathf.Infinity;
            else
                return f;
        }
        
        /*
         * Weight function
         * TODO: Need to have special weight functions or the first two points
         */

        // Weight of p3 depending on the two previous points
        
        public float Weight(int p1)
        {
            return 0f;
        }
        
        public float Weight(int p1, int p2)
        {
            return STransfer(Slope(p1, p2));
        }
        
        public float Weight(int p1, int p2, int p3)
        {
            return STransfer(Slope(p2, p3)) + CTransfer(Curvature(_graph.Position[p1], _graph.Position[p2], _graph.Position[p3]));
        }
    }
}