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

    [Space]
    public Animator animator;
    public float destroyAfter;

    //Target
    [Space]
    public GameObject target;
    public LayerMask layerMask;

    //AI
    private AIDestinationSetter aiDestinationSetter;
    private AIPath aiPath;

    public void SetVariables(GameObject gameObject)
    {
        thisObject = gameObject.GetComponent<GameObject>();
        aiDestinationSetter = thisObject.GetComponent<AIDestinationSetter>();
        aiPath = thisObject.GetComponent<AIPath>();
    }
    public abstract void Attack();
    public void Destroy()
    {
        Destroy(thisObject, destroyAfter);
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
    }
    public void CheckInRangeTarget()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, rangeToAttack, layerMask);
        if (collider != null)
        {
            aiPath.canMove = false;
            return;
        }
        else
        {
            aiPath.canMove = true;
            return;
        }
    }
}
