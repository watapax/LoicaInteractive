using UnityEngine;
using UnityEngine.UI;
public class HiddenObject : MonoBehaviour
{
    public string id;
    public string letra;
    public AudioClip clipOk, clipNo;
    [HideInInspector] public RectTransform rectTransform;
    public Image image;

    public Vector3 startPosition;
    public Vector3 ghostPosition;
    public bool puedeApretarse = true;
    public float escalaObjeto;
    public void AlimentarDatosHiddenObject(ObjetoTemplate _obj, float _escala)
    {
        escalaObjeto = _escala;
        id = _obj.id;
        letra = _obj.letra;
        clipOk = _obj.clipOk;
        clipNo = _obj.clipNo;
        image.sprite = _obj.sprite;
        startPosition = transform.position;
        image.SetNativeSize();
       // rectTransform.localScale = Vector3.one * escalaObjeto;
    }
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void SeApreto()
    {
        if (!puedeApretarse) return;
        HiddenObjectManager.instance.ChequearObjeto(this);
        puedeApretarse = false;
    }
}
