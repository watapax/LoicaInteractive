using UnityEngine;
using DG.Tweening;
public class AnimacionesDoTween : MonoBehaviour
{
    public RectTransform rectTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    void Start()
    {
        rectTransform.localScale = Vector3.zero;
        rectTransform.DOScale(Vector3.one, 1.5f).SetEase(Ease.OutElastic);
    }

    public void Achicar()
    {
        rectTransform.DOScale(Vector3.zero, .6f).SetEase(Ease.InOutSine).onComplete = Desactivar;
    }

    void Desactivar()
    {
        gameObject.SetActive(false);
    }


}
