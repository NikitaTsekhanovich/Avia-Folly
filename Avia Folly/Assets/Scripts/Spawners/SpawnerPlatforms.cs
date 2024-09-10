using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    public class SpawnerPlatforms : MonoBehaviour
    {
        [SerializeField] private Transform _parentComponent;
        private List<Transform> _points = new();
        private List<GameObject> _platforms = new();

        public void SetPlatforms(List<GameObject> platforms, GameObject coordPlatforms)
        {
            _platforms = platforms;

            foreach (var point in coordPlatforms.GetComponentsInChildren<Transform>())
                _points.Add(point);

            SpawnPlatforms();
        }

        private void SpawnPlatforms()
        {
            for (var i = 1; i < _points.Count; i++)
            {
                Instantiate(
                    _platforms[i - 1], 
                    _points[i].transform.position,
                    _points[i].rotation,
                    _parentComponent);
            }
        }
    }
}

