using UnityEngine;

namespace Roads
{
    public class Edge
    {
        public int From { get; set; }
        public int To { get; set; }

        public Edge(int from, int to)
        {
            From = from;
            To = to;
        }
    }
}