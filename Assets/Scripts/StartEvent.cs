using UnityEngine;
using UnityEngine.Events;
public class StartEvent : MonoBehaviour
{
    public UnityEvent onsStart;

    void Start()
    {
        onsStart.Invoke();
    }


}
