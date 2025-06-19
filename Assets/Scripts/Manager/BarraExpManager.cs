using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class BarraExpManager : MonoBehaviour
{
    public static BarraExpManager instance;
    public ExpData expData;
    public Image expBar;
    public UnityEvent onFillExp;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        expBar.fillAmount = expData.prevFill;
    }

    public void SumarExp(float _cantidad)
    {
        expBar.fillAmount += _cantidad;
        CheckIfWin();
    }

    void CheckIfWin()
    {
        if(expBar.fillAmount >= 1)
        {
            onFillExp?.Invoke();
            expData.prevFill = 0;
        }
    }
}
