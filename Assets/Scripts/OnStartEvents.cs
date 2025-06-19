using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class OnStartEvents : MonoBehaviour
{
    public ElBool elBool;
    public UnityEvent onStart;
    public Image localBar, reloj;
    public ExpData expData;
    private void Start()
    {
        if(elBool.value)
        {
            localBar.fillAmount = expData.localExp;
            reloj.fillAmount = expData.tiempoRestante;
            onStart?.Invoke();
        }
    }
}
