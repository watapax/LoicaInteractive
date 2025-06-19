using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System.Data;
using UnityEngine.Events;
public class ExperienceManager : MonoBehaviour
{

    public UnityEvent onSubirdeNivel, onFaltoPoquito, onRespuestaCorrecta, onJugoMal;
    public float velocidadAumentoBarra;
    public ExpData expData;
    public float localExp;
    public Color transparente;
    public Color colorBgActivo;
    public Image expBar, expBarLocal;
    public bool mostrar;
    public TextMeshProUGUI textLvl;
    public Image bg;
    //public float nextExp, currenExp, mod, fill, expAlFinalizarElJuego;
    public float lerpTime;
    public RectTransform panelExp;
    float fillAmountTarget;
    public float porcentajeParaAyudar;
    public GameObject nivelSuperadoObject, faltoPoquitoObject;
    public int objetosEncontrados, totalObjetosEncontrados;

    public void SetTotal(int _total)
    {
        totalObjetosEncontrados = _total;
    }
    private void Start()
    {
       // expData.currentExp = 0;
        //expData.prevFill = 0;
        textLvl.text = "LVL " + expData.lvl.ToString();
        //nextExp = expData.expToNextLvl;
        panelExp.localScale = Vector3.zero;
        bg.color = transparente;
    }
    public void MostrarPanelExp()
    {


        StartCoroutine(FadeColor(bg, colorBgActivo, lerpTime));
        panelExp.DOScale(Vector3.one, lerpTime).SetEase(Ease.InOutSine);
        StartCoroutine(AnimarBarritaExp());
        expData.localExp = 0;
        expData.tiempoRestante = 0;
        elBool.value = false;
    }



    public void ResetLocalExp()
    {
        localExp = 0;
        expBar.fillAmount = expData.prevFill;
    }
    public void GainLocalExp(float _exp)
    {
        objetosEncontrados++;
        localExp += _exp;
        float exp = _exp / expData.expToNextLvl;  //(float)_exp / 100;
        StartCoroutine(AnimarLocalExp(exp));
    }

    IEnumerator AnimarLocalExp(float _exp)
    {
        //Esperar un poquito
        yield return new WaitForSeconds(0.7f); // <-- parametrizar este numero

        float target = expBarLocal.fillAmount + _exp;

        onRespuestaCorrecta.Invoke();

        while(expBarLocal.fillAmount < target)
        {
            expBarLocal.fillAmount += Time.deltaTime * velocidadAumentoBarra;
            yield return null;
        }

        expBarLocal.fillAmount = target;

        //ChequearEstado();

    }







    IEnumerator AnimarBarritaExp()
    {
        yield return new WaitForSeconds(1);

        bool subio = false;
        bool casi = false;
        while (expBarLocal.fillAmount > 0)
        {
            expBarLocal.fillAmount -= Time.deltaTime * velocidadAumentoBarra;
            expBar.fillAmount += Time.deltaTime * velocidadAumentoBarra; // aca debo multiplicar por el conversor de localexp a globalexp

            if(expBar.fillAmount >= 1)
            {
                expBar.fillAmount = 0;
                subio = true;
                SubirDeNivel();
                break;
            }
            yield return null;


        }

        if (!subio && expBar.fillAmount > porcentajeParaAyudar)
        {
            yield return new WaitForSeconds(0.5f);
            faltoPoquitoObject.SetActive(true);
            casi = true;
            //ActivarMinijuegoDeAyuda();


        }

        if (subio)
        {
            yield return new WaitForSeconds(0.5f);
            nivelSuperadoObject.SetActive(true);
        }


        expData.prevFill = expBar.fillAmount;

        if(subio)
        {
            yield return new WaitForSeconds(3);
            onSubirdeNivel.Invoke();
        }
        if(casi)
        {
            yield return new WaitForSeconds(3);
            onFaltoPoquito.Invoke();
        }

        if(expBar.fillAmount < porcentajeParaAyudar)
        {
            onJugoMal?.Invoke();
        }
        yield return null;
    }

    void ActivarMinijuegoDeAyuda()
    {
        print("Se activa el minijuego de ayuda");
    }

    void SubirDeNivel()
    {
        print("animacion feedback de subir de nivel");
        expData.prevFill = 0;
        expData.currentExp = 0;
    }

    IEnumerator FadeColor(Image _image, Color _targetColor, float _lerpTime)
    {
        Color fromColor = _image.color;
        float t = 0;

        while (t < _lerpTime)
        {
            t += Time.deltaTime;
            float p = t / _lerpTime;
            _image.color = Color.Lerp(fromColor, _targetColor, p);
            yield return null;

        }

    }


    public void ChequearEstado()
    {
        if(expBarLocal.fillAmount >= 1)
        {
            // Lleno la barra
            HiddenObjectManager.instance.contador.ToggleContador(false);
            HiddenObjectManager.instance.TerminoMiniJuego();
            MostrarPanelExp();
            return;
        }

        if(objetosEncontrados == totalObjetosEncontrados)
        {
            Shuflear();
            HiddenObjectManager.instance.contador.ToggleContador(false);
            return;
        }

    }

    public bool PararContador()
    {
        bool parar = false;

        if (expBarLocal.fillAmount >= 1)
            parar = true;
        if (objetosEncontrados == totalObjetosEncontrados)
            parar = true;

        return parar;
    }

    void TerminarJuego()
    {

    }
    public UnityEvent onShuflear;
    void Shuflear()
    {
        expData.localExp = expBarLocal.fillAmount;
        expData.tiempoRestante = contador.fillAmount;
        // guardar experienciaLocal

        print("Chufelar!");
        onShuflear?.Invoke();

    }

    private void OnApplicationQuit()
    {
        elBool.value = false;
        expData.localExp = 0;
        expData.tiempoRestante = 0;
    }
    public ElBool elBool;
    public Image contador;
}
