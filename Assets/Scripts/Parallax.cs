using UnityEditor.Rendering;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public static Parallax instance;
    
    public Transform[] capas;
    private bool isDragging = false;
    private Vector3 offset;

    public float minX, maxX;

    // Variables para el movimiento suavizado
    public  float velocity = 0f; // Velocidad actual del movimiento
    public float smoothTime = 0.3f; // Tiempo de suavizado (ajusta según sea necesario)
    private float targetX; // Posición horizontal objetivo
    float prevXPos;
    public float velocidadX;
    public float desaceleracion;
    Vector3 targetPos;
    public float desplazamiento;
    public bool enable = true;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {


        // Inicializa la posición objetivo
        targetX = capas[0].position.x;
    }

    void Update()
    {

        if (!enable) return;
        for (int i = 1; i < capas.Length; i++)
        {
            Vector3 primeraCapaPos = capas[0].position;
            primeraCapaPos.x *= (i * desplazamiento);
            capas[i].position = primeraCapaPos;
        }
        if (!isDragging)
        {
            // Suaviza el movimiento hacia la posición objetivo
            //float newX = Mathf.SmoothDamp(capas[0].position.x, targetX, ref velocity, smoothTime);
            //capas[0].position = new Vector3(newX, capas[0].position.y, capas[0].position.z);
            capas[0].position = Vector3.Lerp(capas[0].transform.position, targetPos, desaceleracion * Time.deltaTime);
        }

    }

    void OnMouseDown()
    {
        if (!enable) return;
        // Calcula el offset entre el punto de clic y la posición del sprite
        offset = capas[0].position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
        velocity = 0f; // Reinicia la velocidad al comenzar a arrastrar
        prevXPos = capas[0].position.x;
    }

    void OnMouseDrag()
    {
        if (!enable) return;
        if (isDragging)
        {
            velocidadX = (capas[0].position.x - prevXPos) / Time.deltaTime ;
            prevXPos = capas[0].position.x;
            // Obtén la posición del mouse en el mundo
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Calcula la nueva posición del sprite solo en el eje X
            float newX = mousePosition.x + offset.x; //Mathf.Clamp(mousePosition.x + offset.x, minX, maxX);

            // Aplica la nueva posición
            capas[0].position = new Vector3(newX, capas[0].position.y, capas[0].position.z);

            // Actualiza la posición objetivo mientras se arrastra
            targetX = newX;
            targetPos = capas[0].position;
            targetPos.x += 2 * (velocidadX * Time.deltaTime);
            targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);


            for (int i = 1; i < capas.Length; i++)
            {
                Vector3 primeraCapaPos = capas[0].position;
                primeraCapaPos.x *= (i * desplazamiento);
                capas[i].position = primeraCapaPos;
            }

        }
    }

    void OnMouseUp()
    {
        if (!enable) return;
        isDragging = false;


        
    }

}
