using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;
public class PanelRespuesta : MonoBehaviour
{
    public float tiempoDeEspera;
    public TextMeshProUGUI tmpro;
    public RectTransform rectTransform;
    public RectTransform rectTransformObject;
    public Image bg;
    public Color transparente;
    public Color colorBgActivo;

    public float lerpTimeVentana, lerpTimeEscala;
    private void Start()
    {
        rectTransform.localScale = Vector3.zero;
        bg.color = transparente;
    }

    public void ActivarVentana(HiddenObject _hiddenObject, bool _esCorrecto)
    {
        
        bg.transform.SetAsLastSibling();
        rectTransform.SetAsLastSibling();
        _hiddenObject.transform.SetAsLastSibling();
        tmpro.text = _hiddenObject.id;
        rectTransform.DOScale(Vector3.one, lerpTimeVentana).SetEase(Ease.InOutSine);
        StartCoroutine( FadeColor(bg, colorBgActivo, lerpTimeVentana));
        _hiddenObject.rectTransform.DOMove(rectTransformObject.position, lerpTimeEscala).SetEase(Ease.InOutSine);
        _hiddenObject.rectTransform.DOScale(rectTransformObject.localScale, lerpTimeEscala).SetEase(Ease.InOutSine);
        
        if(_esCorrecto)
        {
            _hiddenObject.GetComponent<Button>().enabled = false;
        }

        Vector3 targetScale = _esCorrecto ? Vector3.one * 1.5f : Vector3.one ;
        Vector3 targetPos = _esCorrecto ? _hiddenObject.ghostPosition : _hiddenObject.startPosition;

        StartCoroutine(DevolverObjeto(_hiddenObject,targetPos, targetScale));

    }

    IEnumerator DevolverObjeto(HiddenObject _hiddenObject, Vector3 _targetPos, Vector3 _targetScale)
    {
        yield return new WaitForSeconds(tiempoDeEspera);

        FadeColor(bg, transparente, 0.5f);
        rectTransform.DOScale(Vector3.zero, lerpTimeVentana).SetEase(Ease.InOutSine);
        _hiddenObject.rectTransform.DOMove(_targetPos, lerpTimeEscala).SetEase(Ease.InOutSine);
        _hiddenObject.rectTransform.DOScale(_targetScale, lerpTimeEscala).SetEase(Ease.InOutSine);

    }




    IEnumerator FadeColor(Image _image, Color _targetColor, float _lerpTime)
    {
        Color fromColor = _image.color;
        float t = 0;
        
        while(t < _lerpTime)
        {
            t += Time.deltaTime;
            float p = t/_lerpTime;
            _image.color = Color.Lerp(fromColor, _targetColor, p);
            yield return null; 

        }

    }



}
