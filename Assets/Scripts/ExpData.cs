using UnityEngine;

[CreateAssetMenu(fileName = "ExpData", menuName = "Scriptable Objects/ExpData")]
public class ExpData : ScriptableObject
{
    public float lvl, currentExp, expToNextLvl, totalExp;
}
