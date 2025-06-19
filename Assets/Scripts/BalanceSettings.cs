using UnityEngine;

[CreateAssetMenu(fileName = "Balance Settings", menuName = "Scriptable Objects/Balance Settings")]
public class BalanceSettings : ScriptableObject
{

    public float maxTimeForBonus;
    public float puntajeMaximo;
    public float puntajaBase;
    public float minTimeForBonus;

}
