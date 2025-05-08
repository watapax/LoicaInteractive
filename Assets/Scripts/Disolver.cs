using UnityEngine;
using UnityEngine.Events;
public class Disolver : MonoBehaviour
{
    public UnityEvent onBorrar;

    public SpriteRenderer sr;
    bool borrar;
    public float duracionBorrado;
    public Color colorFinal;
    public Color colorInicial;
    float t = 0;
    public bool seBorro;
    private void Start()
    {
        colorInicial = sr.color;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("jabon"))
        {
            borrar = collision.GetComponent<Jabon>().borrando;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("jabon"))
        {
            borrar = collision.GetComponent<Jabon>().borrando;
        }
    }

    private void Update()
    {
        if (seBorro) return;

        if (borrar)
        {
            t += Time.deltaTime;
            float p = t / duracionBorrado;
            sr.color = Color.Lerp(colorInicial, colorFinal, p);

            if(t>= duracionBorrado)
            {
                onBorrar.Invoke();
                seBorro = true;
            }
        }
    }

}
