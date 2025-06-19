using UnityEngine;

[CreateAssetMenu(fileName = "ElBool", menuName = "Scriptable Objects/ElBool")]
public class ElBool : ScriptableObject
{
    public bool value;

    public void SetValue(bool _value)
    {
        value = _value;
    }
}
