using UnityEngine;
using UnityEngine.UI;
public class HubManager : MonoBehaviour
{
    public ExpData expData;
    public Image expBar;

    private void Start()
    {
        expBar.fillAmount = expData.prevFill;
    }
}
