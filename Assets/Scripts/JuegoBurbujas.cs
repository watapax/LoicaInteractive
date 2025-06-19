using UnityEngine;
using UnityEngine.Events;
public class JuegoBurbujas : MonoBehaviour
{
    public static JuegoBurbujas instance;
    public Animator loicaAnimator;
    public GameObject burbuja;
    public Transform pointA, pointB;

    public float minFreq, maxFreq;
    float t;
    float freq;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        freq = Random.Range(minFreq, maxFreq);
    }

    void SpawnearBurbuja()
    {
        float minX = pointA.position.x;
        float maxX = pointB.position.x;
        float x = Random.Range(minX, maxX);
        Vector3 pos = new Vector3(x, pointA.position.y, 0);
        GameObject temp = Instantiate(burbuja, pos, Quaternion.identity);
        temp.transform.parent = transform;
    }

    [ContextMenu("Reventar")]
    public void ReventarTodas()
    {
        Burbuja[] burbujas = GetComponentsInChildren<Burbuja>();
        foreach(Burbuja b in burbujas)
        {
            b.Reventar();
        }
    }


    private void Update()
    {
        t += Time.deltaTime;

        if(t> freq)
        {
            SpawnearBurbuja();
            t = 0;
            freq = Random.Range(minFreq, maxFreq);
        }
    }

    public void AnimarLoica()
    {
        loicaAnimator.SetTrigger("celebra");
    }
}
