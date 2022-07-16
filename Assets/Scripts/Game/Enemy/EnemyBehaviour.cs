using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform target;

    public GameObject tempTarget;
    public Structure structureScript;

    private int damage = 5;

    private float damageCooldown = 2f;
    private float damageTime;
    private void Awake()
    {

        if (target == null)
        {
            
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damageTime = Time.time + damageCooldown;

        try
        {
            target = FindObjectOfType<TownHallStructure>().transform;
            if (target == null)
            {
                target = GameObject.FindWithTag("Player").transform;
            }
        }
        catch
        {
            Debug.Log("Town hall or Player not found");
        }
    }
    void Update()
    {
        if (tempTarget == null)
        {
            transform.LookAt(target.position);
            rb.MovePosition(target.position);
            
        }
        else
        {
            structureScript = tempTarget.GetComponent<Structure>();
            transform.LookAt(tempTarget.transform.position);
            if (Time.time > damageTime)
            {
                Damage();
            }
        }
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<IDamageToFriendly>() != null)
        {
            tempTarget = collision.collider.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<IDamageToFriendly>() != null)
        {
            tempTarget = null;
            structureScript = null;
        }
    }
    private void Damage()
    {
        damageTime = Time.time + damageCooldown;
        structureScript.StructureHealth -= damage;
    }
}
