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

    public void AlimentarDatosHiddenObject(ObjetoTemplate _obj)
    {
        id = _obj.id;
        letra = _obj.letra;
        clipOk = _obj.clipOk;
        clipNo = _obj.clipNo;
        image.sprite = _obj.sprite;
        startPosition = transform.position;
    }
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void SeApreto()
    {
        HiddenObjectManager.instance.ChequearObjeto(this);
    }
}
