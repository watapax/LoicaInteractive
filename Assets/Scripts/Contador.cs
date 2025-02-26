using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
public class Contador : MonoBehaviour
{
    public UnityEvent onComplete;
    public Image imagenContador;
    public float tiempo;
    bool isCounting;


    public void Comenzar()
    {
        if (isCounting) return;
        DOTween.To(() => imagenContador.fillAmount, x => imagenContador.fillAmount = x, 1, tiempo).onComplete = Complete;
        isCounting = true;
    }

    void Complete()
    {
        onComplete.Invoke();
    }



}
