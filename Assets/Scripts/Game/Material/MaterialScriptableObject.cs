using UnityEngine;

[CreateAssetMenu(fileName = "Material", menuName = "Scriptable Objects/New Material")]
public class MaterialScriptableObject : ScriptableObject
{
    public enum MaterialEnum { Wood, Stone, Iron}

    public MaterialEnum materialEnum;
    public int materialHealth;
    public int giveMaterial;
}
