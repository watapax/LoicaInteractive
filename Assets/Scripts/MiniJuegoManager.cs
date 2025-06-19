using UnityEngine;
using UnityEngine.Events;
public class MiniJuegoManager : MonoBehaviour
{
    public UnityEvent onComplete;
    public int totalPuntos;
    int puntos;

    public void SumarPuntos(int _p)
    {
        puntos += _p;

        if(puntos >= totalPuntos )
        {
            onComplete?.Invoke();   
        }
    }
}
