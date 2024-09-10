using System;
using System.Collections.Generic;
using UnityEngine;

public class MainEducation : MonoBehaviour
{
    [SerializeField] private List<GameObject> _images = new();
    private int _index = 1;

    public static Action OnStartEndEducation;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_index < _images.Count)
            {
                if (_index - 1 >= 0)
                    _images[_index - 1].SetActive(false);

                _images[_index].SetActive(true);
                _index++;
            }
            else
            {
                _images[0].SetActive(true);
                _images[_index - 1].SetActive(false);
                _index = 1;
                gameObject.SetActive(false);
                PlayerPrefs.SetString("Education", "true");
                OnStartEndEducation?.Invoke();
            }
        }
    }    
}
