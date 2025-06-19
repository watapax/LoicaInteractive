using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public class Boton : MonoBehaviour
{
    public GameObject prefabSpawn;
    public UnityEvent evento;
    private void OnMouseDown()
    {
        Parallax.instance.enable = false;
    }

    private void OnMouseUp()
    {
        Parallax.instance.enable = true;
        evento?.Invoke();
    }

    public void Achicar()
    {
        
        transform.DOScale(1.2f, 0.2f).OnComplete(() =>
        {
            transform.DOScale(0, 0.4f).SetEase(Ease.InExpo).OnComplete(() =>
            {
                Completar();
            });
               
        });
    }

    public void SumarExp()
    {
        //BarraExpManager.instance.SumarExp(0.02f);
    }

    public void Completar()
    {
        if(prefabSpawn)
        {
            GameObject go = Instantiate(prefabSpawn, transform.position, Quaternion.identity);
            go.transform.localScale = Vector3.one * .5f;
            go.transform.parent = transform.parent;
        }
        BarraExpManager.instance.SumarExp(0.02f);
    }
}
