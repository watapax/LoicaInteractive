using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
public class ExperienceManager : MonoBehaviour
{

    public ExpData expData;
    float localExp;
    public Color transparente;
    public Color colorBgActivo;
    public Image expBar;
    public bool mostrar;
    public TextMeshProUGUI textLvl;
    public Image bg;
    public float nextExp, currenExp, mod, fill, expAlFinalizarElJuego;
    public float lerpTime;
    public RectTransform panelExp;

    private void Start()
    {
        textLvl.text = "LVL " + expData.lvl.ToString();
        nextExp = expData.expToNextLvl;
        panelExp.localScale = Vector3.zero;
        bg.color = transparente;
    }
    public void MostrarPanelExp()
    {
        StartCoroutine(FadeColor(bg, colorBgActivo, lerpTime));
        panelExp.DOScale(Vector3.one, lerpTime).SetEase(Ease.InOutSine);
        currenExp = expData.currentExp;
        expAlFinalizarElJuego = currenExp + localExp;
        mostrar = true;
    }


    float CalcularNextExp()
    {
        return expData.expToNextLvl *= 1.2f; // <== este valor hay que definirlo
    }
    public void ResetLocalExp()
    {
        localExp = 0;
    }
    public void GainLocalExp(int _exp)
    {
        localExp += _exp;
    }


    private void Update()
    {
        if(mostrar)
        {
            expData.currentExp += Time.deltaTime;            
            mod = expData.currentExp % nextExp;
            fill = mod / nextExp;
            expBar.fillAmount = fill;

            if (expData.currentExp > nextExp)
            {
                expData.currentExp = 0;
                nextExp = CalcularNextExp();
                expData.lvl++;
                textLvl.text = "LVL "+expData.lvl.ToString();
            }

            if (currenExp >= expAlFinalizarElJuego)
                mostrar = false;

        }


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


}
