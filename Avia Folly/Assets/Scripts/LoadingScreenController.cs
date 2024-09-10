using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LoadingScreenController : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    private Image _background;

    public static LoadingScreenController instance;

    private void Start() 
    {             
        if (instance == null) 
        { 
            instance = this; 
            DontDestroyOnLoad(gameObject);
        } 
        else 
        { 
            Destroy(this);  
        } 

        _background = _loadingScreen.GetComponent<Image>();
    }

    public void StartAnimationFade()
    {
        _loadingScreen.SetActive(true);

        DOTween.Sequence()
            .Append(_background.DOFade(1f, 1f));
    }

    public void EndAnimationFade()
    {
        DOTween.Sequence()
            .Append(_background.DOFade(1f, 1f));
        
        _loadingScreen.SetActive(false);
    }
}
