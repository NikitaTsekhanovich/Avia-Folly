using System;
using UnityEngine;

namespace Airplanes
{
    public class Destruction : MonoBehaviour
    {
        public static Action<int> OnDecreaseScore;

        private void OnCollisionEnter2D(Collision2D col)
        {           
            if (col.gameObject.TryGetComponent<Aircraft>(out var aircraft))
            {
                aircraft.DoDestroy();
                OnDecreaseScore?.Invoke(1);
            }
        }
    }
}

