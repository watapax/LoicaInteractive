using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections;
public class PanelRespuesta : MonoBehaviour
{
    public Contador contador;
    public float tiempoDeEspera;
    public TextMeshProUGUI tmpro;
    public RectTransform rectTransform;
    public RectTransform rectTransformObject;
    public Image bg;
    public Color transparente;
    public Color colorBgActivo;

    public float lerpTimeVentana, lerpTimeEscala;
    AudioClip audioObjeto;
    private void Start()
    {
        rectTransform.localScale = Vector3.zero;
        bg.color = transparente;
        balance = GetComponent<BalanceHiddenObject>();
    }

    public void ActivarVentana(HiddenObject _hiddenObject, bool _esCorrecto)
    {
        audioObjeto = _esCorrecto? _hiddenObject.clipOk: _hiddenObject.clipNo;
        bg.transform.SetAsLastSibling();
        rectTransform.SetAsLastSibling();
        _hiddenObject.transform.SetAsLastSibling();

        string originalText = _hiddenObject.id;

        char firstChar = originalText[0];

        string restOfText = originalText.Substring(1);

        originalText = $"<color=blue>{firstChar}</color>{restOfText}";

        tmpro.text = originalText;


        rectTransform.DOScale(Vector3.one, lerpTimeVentana).SetEase(Ease.InOutSine);
        StartCoroutine( FadeColor(bg, colorBgActivo, lerpTimeVentana));
        _hiddenObject.rectTransform.DOMove(rectTransformObject.transform.position, lerpTimeEscala).SetEase(Ease.InOutSine);
        _hiddenObject.rectTransform.DOScale(rectTransformObject.localScale, lerpTimeEscala).SetEase(Ease.InOutSine).onComplete = ReproducirSonido;
        
        if(_esCorrecto)
        {
            //_hiddenObject.GetComponent<Button>().enabled = false;
        }

        Vector3 targetScale = _esCorrecto ? Vector3.one * 0.3f : Vector3.one * _hiddenObject.escalaObjeto ;
        Vector3 targetPos = _esCorrecto ? _hiddenObject.ghostPosition : _hiddenObject.startPosition;

        StartCoroutine(DevolverObjeto(_hiddenObject,targetPos, targetScale, _esCorrecto));

    }

    

    void ReproducirSonido()
    {
        print("reproducir sonido");
        if(audioObjeto)
            SoundManager.instance.ReproducirSfx(audioObjeto);
    }

    IEnumerator DevolverObjeto(HiddenObject _hiddenObject, Vector3 _targetPos, Vector3 _targetScale, bool _esCorrecto)
    {

        yield return new WaitForSeconds(tiempoDeEspera);

        StartCoroutine(FadeColor(bg, transparente, 0.5f));
        rectTransform.DOScale(Vector3.zero, lerpTimeVentana).SetEase(Ease.InOutSine);
        _hiddenObject.rectTransform.DOMove(_targetPos, lerpTimeEscala).SetEase(Ease.InOutSine);
        _hiddenObject.rectTransform.DOScale(_targetScale, lerpTimeEscala).SetEase(Ease.InOutSine);

        yield return new WaitForSeconds(lerpTimeEscala);

        _hiddenObject.puedeApretarse = !_esCorrecto;
        balance.lastObjectFoundTime = Time.time;
        if(!HiddenObjectManager.instance.experienceManager.PararContador())
            contador.ToggleContador(true);
        
        HiddenObjectManager.instance.experienceManager.ChequearEstado();

    }
    BalanceHiddenObject balance;



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
