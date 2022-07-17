using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyBehaviour : MonoBehaviour
{
    private AIDestinationSetter destinationSetter;

    private GameObject townHall;
    void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();

        townHall = GameObject.Find("Town Hall");
        if (townHall != null)
        {
            destinationSetter.target = townHall.transform;
        }
        else
        {
            destinationSetter.target = FindObjectOfType<PlayerInventory>().gameObject.transform;
        }
    }
    void Update()
    {

    }
}
