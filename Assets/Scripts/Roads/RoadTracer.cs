using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Roads
{
    public class RoadTracer
    {
        public class Node : IComparable<Node>, IComparer<Node>
        {
            public int   location;
            public Node  parent;
            public int   depth;
            public float f, g, h;

            public Node(int l, Node p)
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
                return x.CompareTo(y);
            }

            public int CompareTo(Node other)
            {
                return f.CompareTo(other.f);
            }
        }

        private Graph           _graph;
        private Terrain.Terrain _land;
        private WeightCalculator _calc;

        public RoadTracer(Graph graph, Terrain.Terrain land)
        {
            _graph = graph;
            _land  = land;
            _calc = new(_graph, _land);
        }

        public List<Vector3> Trace(int src, int dest)
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
                        path.Add(_graph.Position[cursor.location]);
                        cursor = cursor.parent;
                    }

                    return path;
                }

                var children = _graph.GetNeighbor(current.location).Select(point => new Node(point, current));
                foreach (Node child in children)
                {
                    if (clist.Find(node => node.location == child.location) == null)
                    {
                        switch (child.depth)
                        {
                            case 0:
                                child.g = current.g + _calc.Weight(child.location);
                                break;
                            case 1:
                                child.g = current.g + _calc.Weight(child.parent.location, child.location);
                                break;
                            default:
                                child.g = current.g + _calc.Weight(child.parent.parent.location, child.parent.location, child.location);
                                break;
                        }
                        child.h = Vector3.Distance(_graph.Position[child.location], _graph.Position[tloc]);
                        child.f = child.g + child.h;

                        var m = olist.FindIndex(node => node.location == child.location);
                        if (m >= 0)
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
