using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyBehaviour : MonoBehaviour
{
    private AIDestinationSetter aiDestinationSetter;
    private AIPath aiPath;

    private GameObject townHall;
    private GameObject target;

    private bool targetInRange;


    //Enemy Stats
    public LayerMask layerMask;
    public float rangeToAttack;
    public int damage;

    public float nextAttack;
    private float nextAttackTime;

    void Start()
    {
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        aiPath = GetComponent<AIPath>();

        CheckTargetPriority();
    }
    void Update()
    {
        if (CheckInRangeTarget() && Time.time > nextAttackTime)
        {
            Damage();
        }

        CheckTargetPriority();
    }
    private void CheckTargetPriority()
    {
        townHall = GameObject.Find("Town Hall");
        if (townHall != null)
        {
            target = townHall;
            aiDestinationSetter.target = target.transform;
        }
        else
        {
            target = FindObjectOfType<PlayerInventory>().gameObject;
            aiDestinationSetter.target = target.transform;
        }
    }
    private bool CheckInRangeTarget()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, rangeToAttack, layerMask);
        if (collider != null)
        {
            targetInRange = true;
            aiPath.canMove = false;
            return true;
        }
        else
        {
            targetInRange = false;
            aiPath.canMove = true;
            return false;
        }
    }
    private void Damage()
    {
        nextAttackTime = Time.time + nextAttack;

        target.GetComponent<IDamageToFriendly>().Damage(damage);
        Debug.Log("Attack " + gameObject.name);
    }
}
