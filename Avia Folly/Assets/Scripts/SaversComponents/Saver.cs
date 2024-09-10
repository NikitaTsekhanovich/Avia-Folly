using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SaversComponents
{
    public class Saver : MonoBehaviour
    {
        private void Awake() 
        {         
            if (GameObject.FindGameObjectsWithTag("BackgroundMusic").Count() <= 1) 
                DontDestroyOnLoad(gameObject); 
            else 
                Destroy(gameObject); 
        }
    }
}

