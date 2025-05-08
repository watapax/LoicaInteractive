using UnityEngine;
using UnityEngine.Events;
public class Boton : MonoBehaviour
{
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
}
