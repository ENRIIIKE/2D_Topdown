using UnityEngine;
using Pathfinding;
public abstract class EnemyStats : MonoBehaviour, IDamageToEnemy
{
    //General Info
    private GameObject thisObject;
    public int id;
    public string enemyName;

    //Stats
    [Space]
    public int health;
    public float speed;
    public float rangeToAttack;
    public bool targetInRange;
    public bool isDead;

    //Target
    [Space]
    public GameObject target;
    public LayerMask layerMask;

    //Attack
    [Space]
    public float attackTime;
    public float attackCooldown;

    //AI
    private AIDestinationSetter aiDestinationSetter;
    private AIPath aiPath;

    public void SetVariables()
    {
        thisObject = gameObject;
        aiDestinationSetter = thisObject.GetComponent<AIDestinationSetter>();
        aiPath = thisObject.GetComponent<AIPath>();


        aiPath.maxSpeed = speed;
    }
    public abstract void Attack();
    public void Destroy()
    {
        Destroy(thisObject, 0.1f);
    }
    public void CheckTargetPriority()
    {
        target = GameObject.Find("Town Hall");
        if (target != null)
        {
            aiDestinationSetter.target = target.transform;
        }
        else
        {
            target = FindObjectOfType<PlayerInventory>().gameObject;
            aiDestinationSetter.target = target.transform;
        }
    }
    public void GetDamage(int damage)
    {
        if (damage > health)
        {
            damage = health;
            health -= damage;
        }
        else
        {
            health -= damage;
        }
        CheckHealth();
    }
    public void CheckInRangeTarget()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, rangeToAttack, layerMask);
        if (collider != null)
        {
            targetInRange = true;
            aiPath.canMove = false;
            return;
        }
        else
        {
            targetInRange = false;
            aiPath.canMove = true;
            return;
        }
    }
    private void CheckHealth()
    {
        if (health <= 0)
        {
            Destroy();
            GameManager.Instance.enemiesDefeated++;
        }
    }
}
