using UnityEngine;

[CreateAssetMenu(fileName = "New Structure", menuName = "Scriptable Objects/New Structure")]
public class StructureScriptableObject : ScriptableObject
{
    [Header("Basic information needed for structure")]

    public int structureHealth;
    [Space]
    //Structure Cost
    public int structureCostWood;
    public int structureCostStone;
    public int structureCostIron;

    [Header("Change only if the structure is in category Defense")]
    public int structureDamage;
    public int structureArmor;
    public float structureDetectionRange;

    [Header("Change only if the structure is mine")]
    public int damageToMaterial;
    public int giveMaterial;
    public float cooldownTime;
}
