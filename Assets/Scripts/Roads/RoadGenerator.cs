using Terrain;
using Unity.VisualScripting;
using UnityEngine;

namespace Roads
{
    public class RoadGenerator : MonoBehaviour
    {
        [SerializeField]
        private GraphGenerator _gen;

        private RoadTracer _tracer;

        private void Awake()
        {
            _gen.onGraphFilled.AddListener(OnGraphFilled);
        }

        private void OnDestroy()
        {
            if (_gen != null)
            {
                _gen.onGraphFilled.RemoveListener(OnGraphFilled);
            }
        }

        private void OnGraphFilled()
        {
            LineRenderer lr = GetComponent<LineRenderer>();
            _tracer = new RoadTracer(_gen.graph, _gen.land);

            var road = _tracer.Trace(250, 7455);

            lr.startColor = Color.red;
            lr.endColor = Color.blue;
            lr.positionCount = road.Count;
            for (int i = 0; i < road.Count; i++)
            {
                lr.SetPosition(i, road[i]);
            }
        }
    }
}
