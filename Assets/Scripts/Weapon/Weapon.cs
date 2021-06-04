using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    WeaponArchetypes weaponArchetypes;

    GameObject frameInstance;
    GameObject barrelInstance;
    GameObject clipInstance;
    GameObject triggerInstance;

    // Projectile Mechanics
    public int poolSize = 100;
    int poolIndex = 0;
    GameObject projectileObject;
    GameObject[] projectiles;
    WeaponArchetypes.InfusionType projectileInfusion;

    Transform firePoint;

    bool hasWeapon = false;
    bool isFiring = false;

    [Tooltip("Number of shots allowed per second.")]
    public float fireRate = 1f;
    float shotTime;
    float timeSinceLastShot = 0f;
    [Tooltip("How much damage each shot deals.")]
    public float damage = 1f;
    [Tooltip("The radius of the circle that is one unit away from the firing spawn point. The weapon chooses a point on the circle to fire towards.")]
    public float accuracy = 1f;

    public int projectileIndex = 0;

    private void Awake()
    {
        ServiceLocator.Register<Weapon>(this);
    }

    void Start()
    {
        weaponArchetypes = ServiceLocator.Get<WeaponArchetypes>();

        projectiles = new GameObject[poolSize];
    }

    private void OnValidate()
    {
        shotTime = 1.0f / fireRate;
    }

    private void Update()
    {
        UpdateWeaponState();
    }

    public void CreateNewWeapon(Frame f, Barrel b, Clip c, Trigger t, GameObject projectileType = null)
    {
        if (frameInstance)
            Destroy(frameInstance);
        if (barrelInstance)
            Destroy(barrelInstance);
        if (clipInstance)
            Destroy(clipInstance);
        if (triggerInstance)
            Destroy(triggerInstance);

        frameInstance = Instantiate(f.frameObject, transform);
        frameInstance.transform.localScale = new Vector3(f.frameScale, f.frameScale, f.frameScale);
        frameInstance.GetComponent<ElementMaterial>().ChangeElementalMaterials(weaponArchetypes.infusionModuleMaterials[(int)f.infusionType]);
        FrameInfo frameInfo = frameInstance.GetComponent<FrameInfo>(); // Each frame has a frame script that holds transform data for each attachment point for each module on the weapon.
        if (frameInfo == null)
            Debug.LogError("No FrameInfo script found while building weapon.");

        barrelInstance = Instantiate(b.barrelObject, frameInfo.barrelAttachmentTransform);
        barrelInstance.transform.localScale = new Vector3(b.barrelScale, b.barrelScale, b.barrelLength);
        barrelInstance.GetComponent<ElementMaterial>().ChangeElementalMaterials(weaponArchetypes.infusionModuleMaterials[(int)b.infusionType]);
        firePoint = barrelInstance.transform.GetChild(0); // Each barrel has one child which is the firing point of the barrel
        if(firePoint == null)
            Debug.LogError("No FrameInfo script found while building weapon.");

        clipInstance = Instantiate(c.clipObject, frameInfo.clipAttachmentTransform);
        clipInstance.transform.localScale = new Vector3(c.clipScale, c.clipScale, c.clipScale);
        clipInstance.GetComponent<ElementMaterial>().ChangeElementalMaterials(weaponArchetypes.infusionModuleMaterials[(int)c.infusionType]);

        triggerInstance = Instantiate(t.triggerObject, frameInfo.triggerAttachmentTransform);
        triggerInstance.GetComponent<ElementMaterial>().ChangeElementalMaterials(weaponArchetypes.infusionModuleMaterials[(int)t.infusionType]);

        projectileType = weaponArchetypes.DeduceProjectileType(f, b, c, t);

        if (projectileType != null)
        {
            projectileObject = projectileType;
        }

        projectileInfusion = c.infusionType;

        hasWeapon = true;
    }

    public void UpdateWeaponState()
    {
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= shotTime && isFiring)
        {
            timeSinceLastShot = 0;
            FireWeapon();
        }
    }

    public void FireWeapon()
    {
        if(!hasWeapon || projectileObject == null)
        {
            return;
        }

        if(projectiles[poolIndex] != null)
        {
            Destroy(projectiles[poolIndex]);
        }

        // Projectile Settings
        Vector3 initialDirection = firePoint.forward;
        Vector2 random = UnityEngine.Random.insideUnitCircle;

        projectiles[poolIndex] = Instantiate(projectileObject, firePoint.position, firePoint.rotation);
        IProjectileSettings projSettings = projectiles[poolIndex].GetComponent<IProjectileSettings>();
        projSettings.LaunchDirection = initialDirection;
        projSettings.InfusionType = projectileInfusion;
        projSettings.Launch();

        poolIndex++;
        if(poolIndex > poolSize - 1)
        {
            poolIndex = 0;
        }
    }

    public void SetTriggerDown(bool isTriggerDown)
    {
        isFiring = isTriggerDown;
    }
}