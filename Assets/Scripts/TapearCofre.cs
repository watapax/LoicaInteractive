using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
public class TapearCofre : MonoBehaviour
{
    public UnityEvent onAbrirCofre;
    public float shakeDuration = 1f; // Duración del shake
    public float shakeStrength = 0.5f; // Intensidad del shake
    public int shakeVibrato = 10; // Cantidad de vibraciones
    public float shakeRandomness = 90f; // Aleatoriedad del shake
    public bool snapping = false; // Si deseas que el movimiento sea en valores enteros
    public GameObject prefabImpact;
    public Transform abajoIzquierda, arribaDerecha;

    public int hp;
    bool seAbrio;
    private void OnMouseDown()
    {
        if (seAbrio) return;
        transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness, snapping).SetEase(Ease.OutQuad);
        Instantiate(prefabImpact, PosicionRandom(), Quaternion.identity);

        hp--;
        if(hp < 1)
        {
            seAbrio = true;
            onAbrirCofre.Invoke();
        }
    }

    Vector3 PosicionRandom()
    { 
        float x = Random.Range(abajoIzquierda.position.x, arribaDerecha.position.x);
        float y = Random.Range(abajoIzquierda.position.y, arribaDerecha.position.y);
        return new Vector3(x, y, 0);
    }




}
