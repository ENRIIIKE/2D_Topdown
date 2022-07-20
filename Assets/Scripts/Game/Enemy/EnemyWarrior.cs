using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarrior : EnemyStats
{
    private void Update()
    {
        base.CheckTargetPriority();
        base.CheckInRangeTarget();
    }
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
}
