using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour, IProjectileSettings
{
    WeaponArchetypes.InfusionType infusionType;
    public WeaponArchetypes.InfusionType InfusionType { get { return infusionType; } set { infusionType = value; } }

    public List<Renderer> elementalRenderers = new List<Renderer>();
    public List<Renderer> ElementalRenderers { get { return elementalRenderers; } set { elementalRenderers = value; } }
    public Vector3 LaunchDirection { get; set; }

    public float damage = 1f;
    public float speed = 30.0f;
    public float maxLifeTime = 3.0f;
    public int maxBounces = 4;
    int bounces = 0;

    Rigidbody rb;
    WeaponArchetypes weaponArchetypes;

    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    public void Launch()
    {
        rb = GetComponent<Rigidbody>();
        weaponArchetypes = ServiceLocator.Get<WeaponArchetypes>();

        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
        ChangeElementalMaterials(infusionType);
    }

    public void OnCollisionEnter(Collision collision)
    {
        IDamageable objToDamage = collision.gameObject.GetComponent<IDamageable>();
        if (objToDamage != null)
        {
            objToDamage.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        bounces++;
        if (bounces >= maxBounces + 1)
        {
            Destroy(gameObject);
            return;
        }

        ContactPoint cP = collision.GetContact(0);
        transform.forward = Vector3.Reflect(transform.forward, cP.normal);
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
    }

    public void ChangeElementalMaterials(WeaponArchetypes.InfusionType infusionType)
    {
        foreach (var renderer in ElementalRenderers)
        {
            renderer.material = weaponArchetypes.GetMaterialForInfusion(infusionType);
        }
    }
}