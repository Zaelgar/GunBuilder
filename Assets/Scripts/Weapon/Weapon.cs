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
    #region Projectile Types
    public uint flameBounce = (0x1 << 1) << (0x1 << 2);
    public uint novaShot = (0x1 << 2) << (0x1 << 1);
    public uint laser = (0x1 << 1) << (0x1 << 4);
    public uint timedExplodingShots = (0x1 << 4) << (0x1 << 1);
    public uint slowingBullets = (0x1 << 2) << (0x1 << 4);
    public uint railgun = (0x1 << 4) << (0x1 << 2);
    #endregion

    public GameObject projectile;

    #region Weapon References
    public Frame weaponFrame;
    public GameObject frameInstance;

    private FrameInfo frameInfo;

    public Barrel weaponBarrel;
    public GameObject barrelInstance;

    public Clip weaponClip;
    public GameObject clipInstance;

    public Trigger weaponTrigger;
    public GameObject triggerInstance;
    #endregion

    private void Awake()
    {
        ServiceLocator.Register<Weapon>(this);
    }

    private void Start()
    {
        // Instantiate all weapon pieces.
        frameInstance = Instantiate(weaponFrame.frame, transform);
        frameInfo = weaponFrame.GetFrameInfo();
        barrelInstance = Instantiate(weaponBarrel.barrel, frameInfo.GetBarrelAttachmentTransform().position + frameInstance.transform.position, transform.rotation, frameInstance.transform);
        clipInstance = Instantiate(weaponClip.clip, frameInfo.GetClipAttachmentTransform().position + transform.position, transform.rotation, frameInstance.transform);
        triggerInstance = Instantiate(weaponTrigger.trigger, frameInfo.GetTriggerAttachmentTransform().position + transform.position, transform.rotation, frameInstance.transform);
    }

    public void OnSlotChange()
    {
        DeduceProjectileType();
    }

    private void DeduceProjectileType()
    {
        // Weapons are currently only affected by barrel type, and then by clip type.
        uint fireType = (uint)weaponBarrel.infusionType;

        fireType <<= (int)weaponClip.ApplyModule();

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

    #region Set Module Public Functions
    public void SetFrame(Frame f)
    {
        weaponFrame = f;
        // TODO
        //DeduceProjectileType(); - Not needed for now
        Instantiate(weaponFrame, transform);
        frameInfo = weaponFrame.frame.GetComponent<FrameInfo>();
        if(frameInfo == null)
        {
            Debug.LogError("No Frame info for this frame!");
        }
    }

    public void SetBarrel(Barrel b)
    {
        weaponBarrel = b;
        DeduceProjectileType();
    }

    public void SetClip(Clip c)
    {
        weaponClip = c;
        DeduceProjectileType();
    }

    public void SetTrigger(Trigger t)
    {
        weaponTrigger = t;
        // TODO
        //DeduceProjectileType(); - Not needed for now
    }
    #endregion

    // This function serves to reposition other modules once a different weapon frame is used.
    private void RepositionModules()
    {
        // TODO
    }

    public Transform GetClipTransform()
    {
        return frameInfo.GetClipAttachmentTransform();
    }

    public Transform GetTriggerTransorm()
    {
        return frameInfo.GetTriggerAttachmentTransform();
    }

    public Transform GetBarrelTransform()
    {
        return frameInfo.GetBarrelAttachmentTransform();
    }
}