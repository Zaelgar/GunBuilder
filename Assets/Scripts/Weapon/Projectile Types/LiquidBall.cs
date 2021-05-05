using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidBall : MonoBehaviour, IProjectileSettings, IElementalMaterial
{
    WeaponArchetypes.InfusionType infusionType;
    public WeaponArchetypes.InfusionType InfusionType { get { return infusionType; } set { infusionType = value; } }

    public List<Renderer> elementalRenderers = new List<Renderer>();
    public List<Renderer> ElementalRenderers { get { return elementalRenderers; } set { elementalRenderers = value; } }
    public Vector3 LaunchDirection { get; set; }

    public float damage = 10f;
    public float explosionRadius = 2f;
    public float speed = 30.0f;
    public float maxLifeTime = 5.0f;
    public int maxBounces = 6;

    Rigidbody rb;
    WeaponArchetypes weaponArchetypes;
    Collider myCollider;
    public ParticleSystem explosionEffect;

    void Start()
    {
        myCollider = GetComponent<Collider>();
        Destroy(gameObject, maxLifeTime);
    }

    public void Launch()
    {
        weaponArchetypes = ServiceLocator.Get<WeaponArchetypes>();
        rb = GetComponent<Rigidbody>();

        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
        ChangeElementalMaterials(infusionType);
    }

    void OnCollisionEnter(Collision collision)
    {
        // When we collide with something, we explode and deal damage
        Collider[] collidersHit = Physics.OverlapSphere(transform.position, explosionRadius);
        explosionEffect.Play();

        foreach (var col in collidersHit)
        {
            if (col == myCollider)
                continue;

            IDamageable objToDamage = col.gameObject.GetComponent<IDamageable>();
            if (objToDamage != null)
            {
                objToDamage.TakeDamage(damage);
            }
        }
    }

    public void ChangeElementalMaterials(WeaponArchetypes.InfusionType infusionType)
    {
        foreach (var renderer in ElementalRenderers)
        {
            renderer.material = weaponArchetypes.GetMaterialForInfusion(infusionType);
        }
    }
}