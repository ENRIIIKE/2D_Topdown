using UnityEngine;
using enrike.utils.text;

public class Structure : MonoBehaviour, IDamageToFriendly
{
    public StructureScriptableObject structureSO;

    private int structureHealth;
    public int StructureHealth
    {
        get { return structureHealth; }
        set { structureHealth = value; }
    }
    
    #region StructureCost
    //Structure Cost
    private int structureMaterialCostWood;
    public int StructureMaterialCostWood
    {
        get { return structureMaterialCostWood; }
        private set { structureMaterialCostWood = value; }
    }
    private int structureMaterialCostStone;
    public int StructureMaterialCostStone
    {
        get { return structureMaterialCostStone; }
        private set { structureMaterialCostStone = value; }
    }
    private int structureMaterialCostIron;
    public int StructureMaterialCostIron
    {
        get { return structureMaterialCostIron; }
        private set { structureMaterialCostIron = value; }
    }
    #endregion

    public bool preview;

    //Find other solution
    private Turret turretScript;
    private MineStructure mineScript;
    private TownHallStructure townHallStructure;

    private void Awake()
    {
        StructureHealth = structureSO.structureHealth;

        StructureMaterialCostWood = structureSO.structureCostWood;
        StructureMaterialCostStone = structureSO.structureCostStone;
        StructureMaterialCostIron = structureSO.structureCostIron;
    }
    private void Start()
    {
        //If the structure is not for preview only then take material from player's inventory

        if (preview) return;

        PlayerInventory.Instance.TakeMaterial(structureMaterialCostWood, 1);
        PlayerInventory.Instance.TakeMaterial(structureMaterialCostStone, 2);
        PlayerInventory.Instance.TakeMaterial(structureMaterialCostIron, 3);


        EnableStructureFunction();
    }
    public void Damage(int damage)
    {
        if (damage > StructureHealth)
        {
            damage = StructureHealth;
            StructureHealth -= damage;
        }
        else
        {
            StructureHealth -= damage;
        }
        CheckHealth();
    }
    private void EnableStructureFunction()
    {
        try
        {
            turretScript = GetComponent<Turret>();
            mineScript = GetComponent<MineStructure>();
            townHallStructure = GetComponent<TownHallStructure>();
        }
        catch
        {
            Debug.Log("Didn't find structure core mechanic");
        }

        if (turretScript != null)
        {
            turretScript.enabled = true;
        }
        else if (mineScript != null)
        {
            mineScript.enabled = true;
        }
        else if (townHallStructure != null)
        {
            townHallStructure.enabled = true;
        }
    }
    private void CheckHealth()
    {
        if (StructureHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
