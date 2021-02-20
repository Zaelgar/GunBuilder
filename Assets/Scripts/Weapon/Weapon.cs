using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*

Weapon Attributes:

- Accuracy (default radius for shooting cone)
- Recoil (the amount of expansion per shot of the accuracy shooting cone)
- Damage (the amount of damage that a single shot will do)
- Clip Size (the amount of ammo inside of one clip)
- Ammo Capacity (the amount of bullets the player can carry for a weapon)

*/

public class Weapon : MonoBehaviour
{
    // Fire barrel, magnet ammo
    public uint flameBounce = (0x1 << 1) << (0x1 << 2);
    public uint novaShot = (0x1 << 2) << (0x1 << 1);
    public uint laser = (0x1 << 1) << (0x1 << 4);
    public uint timedExplodingShots = (0x1 << 4) << (0x1 << 1);
    public uint slowingBullets = (0x1 << 2) << (0x1 << 4);
    public uint railgun = (0x1 << 4) << (0x1 << 2);

    public List<Module> modules;
    public GameObject projectile;

    public Frame weaponFrame;
    public Barrel weaponBarrel;
    public Clip weaponClip;
    public Trigger weaponTrigger;

    private void Awake()
    {
        ServiceLocator.Register<Weapon>(this);
    }

    private void Start()
    {
        WeaponSlot.OnSlotChange += OnSlotChange;

        SortModules();
        weaponFrame = modules[0] as Frame;
        weaponBarrel = modules[1] as Barrel;
        weaponClip = modules[2] as Clip;
        weaponTrigger = modules[3] as Trigger;
    }

    public void OnSlotChange()
    {
        DeduceProjectileType();
    }
    private void DeduceProjectileType()
    {
        SortModules();

        bool isFirst = false;
        uint fireType = 0;

        foreach(Module mod in modules)
        {
            if(mod.infusionType != Module.InfusionType.NONE)
            { 
                if (!isFirst)
                {
                    fireType = mod.ApplyModule();
                    isFirst = true;
                    continue;
                }
                fireType <<= (int)mod.ApplyModule();
            }
        }

        Debug.Log(fireType);

        switch (fireType)
        {
            case (0x1 << 1) << (0x1 << 2):
                Debug.Log("Flame Bounce Shot");
                break;

            case (0x1 << 2) << (0x1 << 1):
                Debug.Log("Nova Shots");
                break;

            case (0x1 << 1) << (0x1 << 4):
                Debug.Log("Laser");
                break;

            case (0x1 << 4) << (0x1 << 1):
                Debug.Log("Timed Explosion");
                break;

            case (0x1 << 2) << (0x1 << 4):
                Debug.Log("Slowing Bullets");
                break;

            case (0x1 << 4) << (0x1 << 2):
                Debug.Log("Railgun");
                break;

            default:
                Debug.LogWarning("No Suitable Deduction Found!");
                break;
        }
    }


    private void SortModules()
    {
        modules.Sort((mod1, mod2) => mod1.moduleType.CompareTo(mod2.moduleType));
    }

    public void SetBarrel(Barrel b)
    {
        weaponBarrel = b;
        modules[1] = b;
        DeduceProjectileType();
    }
}