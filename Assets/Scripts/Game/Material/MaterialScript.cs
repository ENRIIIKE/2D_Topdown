using UnityEngine;

public class MaterialScript : MonoBehaviour, IDamageToMaterial
{
    public MaterialScriptableObject materialSO;
    public GameObject particles;
    private Transform particlesParent;

    private int health;
    public int Health
    {
        get { return health; }
        private set { health = value; }
    }

    [Tooltip("1 = Wood; 2 = Stone; Iron = 3")]
    public int materialIndex;

    private int amountToGive;

    void Start()
    {
        particlesParent = GameObject.Find("Particles Parent").transform;
        Health = materialSO.materialHealth;
        amountToGive = materialSO.giveMaterial;
    }

    //Damage to material with axe
    public void DamageToMaterial(int damage)
    {
        if (damage > Health)
        {
            damage = Health;
            Health -= damage;
        }
        else
        {
            Health -= damage;
        }

        GameObject particle = Instantiate(particles, transform.position, Quaternion.identity, particlesParent);
        float durationToDestroy = particle.GetComponent<ParticleSystem>().main.startLifetime.constant;
        Destroy(particle, durationToDestroy);

        PlayerInventory.Instance.GiveMaterial(amountToGive, materialIndex);
        CheckHealth();
    }

    //Checks if health is belowed or equal to zero, if so then destroy
    private void CheckHealth()
    {
        if (Health <= 0)
        {
            Destroy(gameObject, 0.1f);

            GameObject particle = Instantiate(particles, transform.position, Quaternion.identity, particlesParent);

            float durationToDestroy = particle.GetComponent<ParticleSystem>().main.startLifetime.constant;
            Destroy(particle, durationToDestroy);
        }
    }
}
