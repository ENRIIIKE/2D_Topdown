using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    public float maxDistance;
    public Turret turretScript;

    private Vector3 lastPosition;
    private float totalDistance;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        float distance = Vector3.Distance(lastPosition, transform.position);
        totalDistance += distance;
        lastPosition = transform.position;

        if (totalDistance >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageToEnemy damageToEnemy = collision.gameObject.GetComponent<IDamageToEnemy>();

        if (damageToEnemy != null)
        {
            damageToEnemy.GetDamage(turretScript.TurretDamage);
        }
    }
}
