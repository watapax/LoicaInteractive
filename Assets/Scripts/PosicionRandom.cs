using UnityEngine;

public class PosicionRandom : MonoBehaviour
{
    Vector3 startPosition = Vector3.zero;
    public float radio;
    public float frecuencia;
    public float velocidadMovimiento;
    Vector3 nuevaPosicion;
    float t = 0;

    void AsignarNuevaPosicion()
    {
        float x = Random.Range(-1, 1);
        float y = 0;

        Vector2 direccion = new Vector2(x, y);
        direccion *= radio;
        nuevaPosicion = direccion;
    }

    private void Update()
    {
        t += Time.deltaTime;
        if(t > frecuencia)
        {
            AsignarNuevaPosicion();
            t = 0;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, nuevaPosicion, velocidadMovimiento);
    }
}
