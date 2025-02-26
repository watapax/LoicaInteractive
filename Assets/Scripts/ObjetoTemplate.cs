using UnityEngine;

[CreateAssetMenu(fileName = "ObjetoTemplate", menuName = "Scriptable Objects/ObjetoTemplate")]
public class ObjetoTemplate : ScriptableObject
{
    public string id;
    public string letra;
    public AudioClip clipOk, clipNo;
    public Sprite sprite;

}
