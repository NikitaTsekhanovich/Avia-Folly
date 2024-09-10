using System.Collections.Generic;
using UnityEngine;

namespace Airplanes
{
    public class Line : MonoBehaviour
    {
        [SerializeField] private LineRenderer _render;
        private readonly Queue<Vector2> _points = new();

        public void SetPosition(Vector2 pos)
        {
            if (!CanAppend(pos)) return;

            _points.Enqueue(pos);

            _render.positionCount++;
            _render.SetPosition(_render.positionCount - 1, pos);
        }

        public void RemovePosition()
        {
            _points.Dequeue();

            _render.positionCount = 0;
            foreach (var a in _points)
            {
                _render.positionCount++;
                _render.SetPosition(_render.positionCount - 1, a);
            }
        }

        public Queue<Vector2> GetPointsLine()
        {
            return _points;
        }

        private bool CanAppend(Vector2 pos)
        {
            if (_render.positionCount == 0) return true;

            return Vector2.Distance(_render.GetPosition(_render.positionCount - 1), pos) > .1f;
        }
    }
}

