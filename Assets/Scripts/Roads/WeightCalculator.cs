/*
 * Probably refactor those to include them in the Graph class
 *
 * Source : https://onlinelibrary.wiley.com/doi/full/10.1111/j.1467-8659.2009.01612.x
 */

using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

namespace Roads
{
    public static class WeightCalculator
    {
        /*
         * Characteristic functions
         */

        // Calculate slope between two points, starting from p1 to p2
        // So if p1 is above p2 slope is negative and vice versa
        public static float Slope(Vector3 p1, Vector3 p2)
        {
            return 0f;
        }

        // Calculate the angle formed by p1, p2 and p3
        public static float Curvature(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            return 0f;
        }
        
        /*
         * Transfer functions
         */
        
        public static float WhTransfer(float wh)
        {
            return 0f;
        }

        public static float STransfer(float s)
        {
            return 0f;
        }

        public static float CTransfer(float c)
        {
            return 0f;
        }
        
        /*
         * Weight function
         * TODO: Need to have special weight functions or the first two points
         */

        // Weight of p3 depending on the two previous points
        
        public static float Weight(Vector3 p1)
        {
            return 0f;
        }
        
        public static float Weight(Vector3 p1, Vector3 p2)
        {
            return 0f;
        }
        
        public static float Weight(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            return 0f;
        }
    }
}