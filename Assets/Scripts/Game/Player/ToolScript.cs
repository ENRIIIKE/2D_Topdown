using UnityEngine;

public class ToolScript : MonoBehaviour
{
    public PlayerInteraction playerInteraction;

    [Space]
    public int toolDamage;
    public Transform sphereCheck;
    public float sphereRadius;
    public LayerMask layerMask;

    private Collider2D[] colliders;

    public void CheckCollision()
    {
        colliders = Physics2D.OverlapCircleAll(sphereCheck.position, sphereRadius, layerMask);

        if(colliders.Length > 0)
        {
            DealDamage();
        }

        colliders = new Collider2D[0]; 
    }
    private void DealDamage()
    {
        foreach(Collider2D collider in colliders)
        {
            IDamageToMaterial damageable = collider.GetComponentInParent<IDamageToMaterial>();
            damageable.Damage(toolDamage);
        }
    }

    #region Hide
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(sphereCheck.position, sphereRadius);
    }
    public void ChangeBool()
    {
        playerInteraction.axeReadyToUse = true;
        playerInteraction.ableToSwitch = true;
    }
    #endregion
}
