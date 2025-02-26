using UnityEngine;
using UnityEngine.Events;
public class DropAction : MonoBehaviour
{
    public UnityEvent elEvento;
    public string id;
    public bool isOverlaping;
    bool isDraging;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("dropeables"))
        {
            isOverlaping = collision.GetComponent<DropAction>().id == id;
            if(isOverlaping )
            {
                InteractionSystem.OnDrop += RecibirNotificacion;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("dropeables"))
        {
            isOverlaping = false;
            InteractionSystem.OnDrop -= RecibirNotificacion;
        }
    }

    public void RecibirNotificacion()
    {
        if (isOverlaping)
        {
           // implementar la wea del objeto print(gameObject.name);
           // gatillar un evento publico
           elEvento.Invoke();
        }
    }
}
