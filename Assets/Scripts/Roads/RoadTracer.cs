using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Roads
{
    public class RoadTracer
    {
        public class Node : IComparer<Node>
        {
            public Vector3 location;
            public Node    parent;
            public int     depth;
            public float   f, g, h;

            public Node(Vector3 l, Node p)
            {
                location = l;
                parent   = p;

                if (p != null)
                    depth = p.depth + 1;
                else
                    depth = 0;
            }

            public int Compare(Node x, Node y)
            {
                return x.f == y.f ? 0 : x.f > y.f ? 1 : -1;
            }
        }

        private Graph _graph;

        public RoadTracer(Graph graph)
        {
            _graph = graph;
        }

        public List<Vector3> Trace(Vector3 src, Vector3 dest)
        {
            var sloc = src;
            var tloc = dest;

            List<Node> clist = new();
            List<Node> olist = new() { new(sloc, null) };

            while (olist.Count > 0)
            {
                olist.Sort();

                var current = olist[0];

                olist.RemoveAt(0);
                clist.Add(current);

                if (current.location == tloc)
                {
                    List<Vector3> path = new();

                    var cursor = current;
                    while (cursor != null)
                    {
                        path.Add(cursor.location);
                        cursor = cursor.parent;
                    }

                    return path;
                }

                var children = _graph.GetNeighbor(current.location).Select(point => new Node(point, current));
                foreach (Node child in children)
                {
                    if (clist.Find(node => node.location == child.location) != null)
                    {
                        switch (child.depth)
                        {
                            case 0:
                                child.g = current.g + WeightCalculator.Weight(child.location);
                                break;
                            case 1:
                                child.g = current.g + WeightCalculator.Weight(child.parent.location, child.location);
                                break;
                            default:
                                child.g = current.g + WeightCalculator.Weight(child.parent.parent.location, child.parent.location, child.location);
                                break;
                        }
                        child.h = Vector3.Distance(child.location, tloc);
                        child.f = child.g + child.h;

                        var m = olist.FindIndex(node => node.location == child.location);
                        if (m != -1)
                        {
                            if (child.g < olist[m].g)
                                olist[m] = child;
                        }
                        else
                        {
                            olist.Add(child);
                        }
                    }
                }
            }

            throw new Exception("Unable to find a path.");
        }
    }
}
