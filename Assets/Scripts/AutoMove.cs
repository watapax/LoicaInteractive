using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public Vector2 direccion;
    public float  minSpeed, maxSpeed;
    float speed;
    private void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        transform.Translate(direccion * speed * Time.deltaTime);
    }
}
