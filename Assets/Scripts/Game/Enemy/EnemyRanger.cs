using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanger : EnemyStats
{
    private void Start()
    {
        SetVariables();
    }
    private void Update()
    {
        base.CheckTargetPriority();
        base.CheckInRangeTarget();

        if (base.targetInRange && Time.time > attackTime)
        {
            Attack();
        }
    }
    public override void Attack()
    {
        attackTime = Time.time + attackCooldown;
        Debug.Log("Attack");
    }
}
