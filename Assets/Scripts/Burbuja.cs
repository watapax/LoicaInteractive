using UnityEngine;

using UnityEngine.Events;
public class Burbuja : MonoBehaviour
{
    public float puntos;
    public UnityEvent onPop;
    public Animator animator;
    public float minSpeed = 0.8f;
    public float maxSpeed = 1.2f;
    string currentAnimationName;
    public float velocidadMin, velocidadMax;
    public float escalaMax, escalaMin;
    float velocidad;
    private void Start()
    {
        float escala = Random.Range(escalaMin, escalaMax);
        transform.localScale = Vector3.one * escala;
        velocidad = Random.Range(velocidadMin, velocidadMax);

        velocidad *= Random.Range(0, 100) > 50 ? 1 : -1;

        if (animator.runtimeAnimatorController != null)
        {
            AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            if (clipInfo.Length > 0)
            {
                currentAnimationName = clipInfo[0].clip.name;
            }
        }

        SetRandomSpeed();
    }

    void SetRandomSpeed()
    {
        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        animator.speed = randomSpeed;

        // Opcional: reiniciar la animación para aplicar el cambio de velocidad inmediatamente
        if (!string.IsNullOrEmpty(currentAnimationName))
        {
            animator.Play(currentAnimationName, 0, 0f);
        }
    }

    public void RegenerateSpeed()
    {
        SetRandomSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * velocidad * Time.deltaTime);
    }


    private void OnMouseDown()
    {
        JuegoBurbujas.instance.AnimarLoica();
        BarraExpManager.instance.SumarExp(puntos);
        Reventar();
    }

    public void Reventar()
    {
        onPop?.Invoke();
    }
}
