using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour, IProjectileSettings
{
    public Vector3 LaunchDirection { get; set; }

    public WeaponArchetypes.InfusionType infusionType;
    public WeaponArchetypes.InfusionType InfusionType { get { return infusionType; } set { infusionType = value; } }

    public List<Renderer> elementalRenderers = new List<Renderer>();
    public List<Renderer> ElementalRenderers { get { return elementalRenderers; } set { elementalRenderers = value; } }

    public float damage = 1f;
    public float speed = 30.0f;
    public float maxLifeTime = 3.0f;

    Rigidbody rb;
    WeaponArchetypes weaponArchetypes;

    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    public void Launch()
    {
        weaponArchetypes = ServiceLocator.Get<WeaponArchetypes>();
        rb = GetComponent<Rigidbody>();

        ChangeElementalMaterials(InfusionType);
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
    }

    void OnCollisionEnter(Collision collision)
    {
        IDamageable objToDamage = collision.gameObject.GetComponent<IDamageable>();
        if (objToDamage != null)
        {
            objToDamage.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

    public void ChangeElementalMaterials(WeaponArchetypes.InfusionType infusionType)
    {
        foreach(var renderer in ElementalRenderers)
        {
            renderer.material = weaponArchetypes.GetMaterialForInfusion(infusionType);
        }
    }
}