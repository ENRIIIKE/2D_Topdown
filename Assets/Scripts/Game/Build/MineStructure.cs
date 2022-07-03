using UnityEngine;
using enrike.utils.text;
public class MineStructure : MonoBehaviour
{
    private GameObject miningObject;
    private Structure structureScript;
    public LayerMask layerMask;
    private float cooldownTime;
    private float nextMineTime;
    private int damage;
    private int giveMaterial;

    public float fontSize;

    void Start()
    {
        structureScript = GetComponent<Structure>();

        cooldownTime = structureScript.structureSO.cooldownTime;
        nextMineTime = Time.time + cooldownTime;

        damage = structureScript.structureSO.damageToMaterial;
        giveMaterial = structureScript.structureSO.giveMaterial;

        Collider2D collider = Physics2D.OverlapCircle(transform.position, 1.2f, layerMask);
        miningObject = collider.gameObject;
    }

    void Update()
    {
        if (Time.time > nextMineTime)
        {
            MineMaterial();
        }
    }
    private void MineMaterial()
    {
        nextMineTime = Time.time + cooldownTime;

        miningObject.GetComponent<MaterialScript>().DamageToMaterial(damage);

        PlayerInventory.Instance.GiveMaterial(giveMaterial, 3);
        string textWhenMined = "+" + giveMaterial + " " + miningObject.name;

        UniversalUtilities.InstantiateText(transform.position, textWhenMined, fontSize, Color.white);
    }
}
