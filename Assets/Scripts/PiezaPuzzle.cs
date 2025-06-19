using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UIElements;
public class PiezaPuzzle : MonoBehaviour
{
    public UnityEvent onCalzar;
    public Transform pointTransform;
    public float distanciaMin = 0.1f;

    Vector3 finalPos, startPos;
    Quaternion finalRot;
    public float duracion = 0.5f;
    private void Start()
    {
        startPos = transform.position;
        finalPos = pointTransform.position;
        finalRot = pointTransform.rotation;
    }
    public void CheckCalce()
    {
       float d = Vector3.Distance(transform.position, pointTransform.position);

        if(d <= distanciaMin )
        {
            transform.DOMove(finalPos, duracion);
            transform.DORotate(Vector3.zero, duracion);
            onCalzar?.Invoke();
        }
        else
        {
            transform.DOMove(startPos, duracion);
            transform.DORotate(Vector3.zero, duracion);
        }
    }

}
