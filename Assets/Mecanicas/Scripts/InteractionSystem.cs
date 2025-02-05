using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    // Definir el delegado
    public delegate void DropEvents();

    // Crear el evento basado en el delegado
    public static event DropEvents OnDrop;

    public static void GatillarOnDrop()
    {
        OnDrop?.Invoke();
    }
}
