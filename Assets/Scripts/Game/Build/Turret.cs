using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform barrel;
    public GameObject projectilePrefab;
    public GameObject turretParticlePrefab;
    private GameObject particleParent;
    private GameObject temporaryParent;
    private StructureScriptableObject structureOS;

    [Space]
    //Turret Damage
    private int turretDamage;
    public int TurretDamage
    {
        get { return turretDamage; }
        private set { turretDamage = value; }
    }
    public float bulletForce;

    [Space]
    //Enemy detection
    private float turretRange;
    public float TurretRange
    {
        get { return turretRange; }
        private set { turretRange = value; }
    }
    private GameObject detectedEnemy;
    public LayerMask enemeyLayer;

    [Space]
    public float cooldownTime;
    private float nextFireTime = 0;
    public float maxDistanceBulletCanTravel;
    private void Start()
    {
        particleParent = GameObject.Find("Particles Parent");

        structureOS = GetComponent<Structure>().structureSO;
        temporaryParent = GameObject.Find("Temporary Projectiles");

        TurretDamage = structureOS.structureDamage;
        TurretRange = structureOS.structureDetectionRange;
    }
    void Update()
    {
        if (detectedEnemy == null)
        {
            DetectEnemy();
        }
        else if (detectedEnemy != null)
        {
            float distance = Vector2.Distance(transform.position, detectedEnemy.transform.position);

            if (distance > turretRange)
            {
                detectedEnemy = null;
                return;
            }

            if (Time.time > nextFireTime)
            {
                Shoot();
            }
        }
    }

    private void DetectEnemy()
    {
        try
        {
            Collider2D enemyCollider = Physics2D.OverlapCircle(transform.position, TurretRange, enemeyLayer);
            GameObject enemy = enemyCollider.gameObject;

            detectedEnemy = enemy;
        }
        catch
        {

        }
    }
    private void Shoot()
    {
        nextFireTime = Time.time + cooldownTime;

        GameObject projectile = Instantiate(projectilePrefab, barrel.position, barrel.rotation, temporaryParent.transform);
        GameObject particle = Instantiate(turretParticlePrefab, barrel.position, Quaternion.identity, particleParent.transform);
        ParticleSystem particleSystem = particle.GetComponent<ParticleSystem>();

        Vector3 direction = detectedEnemy.transform.position - barrel.transform.position;

        float duration = particleSystem.main.duration;
        BulletDestroy projectileScript = GetComponent<BulletDestroy>();
        projectileScript.maxDistance = maxDistanceBulletCanTravel;
        projectileScript.turretScript = this;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);

        Destroy(particle, duration);
    }
}
