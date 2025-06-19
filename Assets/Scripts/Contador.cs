using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
public class Contador : MonoBehaviour
{
    public UnityEvent onComplete;
    public Image imagenContador;
    public float tiempo;
    public float tiempoRestante;
    bool isCounting;
    bool contando;

    public void Comenzar()
    {
        if (isCounting) return;
        contando = true;
        StartCoroutine(ActivarContador()); // = DOTween.To(() => imagenContador.fillAmount, x => imagenContador.fillAmount = x, 1, tiempo).onComplete = Complete;
        //DOTween.To(() => imagenContador.fillAmount, x => imagenContador.fillAmount = x, 1, tiempo).onComplete = Complete;
        isCounting = true;
        
    }

    void Complete()
    {
        onComplete.Invoke();
    }

    public void ToggleContador(bool _state)
    {
        contando = _state;
    }

    IEnumerator ActivarContador()
    {
        float t = 0;
        float to = tiempo;

        yield return new WaitForSeconds(0.7f);

        while(imagenContador.fillAmount < 1)
        {
            imagenContador.fillAmount += contando ? Time.deltaTime / tiempo : 0;
            yield return null;
        }

        /*
        while(t < to)
        {
            t += contando? Time.deltaTime : 0;
            float p = t / to;
            imagenContador.fillAmount = p;
            yield return null;
        }
        */

        Complete();
    }



}
