using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkTable : MonoBehaviour
{
    #region Projectile Types
    public uint flameBounce = (0x1 << 1) << (0x1 << 2);
    public uint novaShot = (0x1 << 2) << (0x1 << 1);
    public uint laser = (0x1 << 1) << (0x1 << 4);
    public uint timedExplodingShots = (0x1 << 4) << (0x1 << 1);
    public uint slowingBullets = (0x1 << 2) << (0x1 << 4);
    public uint railgun = (0x1 << 4) << (0x1 << 2);
    #endregion

    public FrameSlot frameSlot;
    public BarrelSlot barrelSlot;
    public ClipSlot clipSlot;
    public TriggerSlot triggerSlot;

    public Transform weaponAnchorTransform;

    GameObject weapon;
    Weapon wScript;
    FrameInfo fInfo;
    WeaponArchetypes weaponArchetypes;

    private void Awake()
    {
        ServiceLocator.Register<WorkTable>(this);
    }

    private void Start()
    {
        weaponArchetypes = ServiceLocator.Get<WeaponArchetypes>();
        if(weaponArchetypes == null)
        {
            Debug.LogError("Weapon Archetypes Not Found.");
        }

        // MakeWeapon();
    }

    private void MakeWeapon()
    {
        //weapon = new GameObject();
        //weapon.name = "Created Weapon";
        //wScript = weapon.AddComponent<Weapon>();
        //weapon.transform.position = weaponAnchorTransform.position;
        //weapon.transform.rotation = weaponAnchorTransform.rotation;

        //// Frame is instantiated first, so others can snap to specific frame transforms
        //GameObject frame = weaponArchetypes.framesList[(int)frameSlot.slotFrameAsset.frameType];
        //Transform fTransform = weapon.transform;
        //wScript.frameInstance = Instantiate(frame, fTransform);
        //fInfo = wScript.frameInstance.GetComponent<FrameInfo>();
        //wScript.frameInfo = fInfo;

        //// Instantiate Barrel
        //GameObject barrel = weaponArchetypes.barrelsList[(int)barrelSlot.slotBarrelAsset.barrelType];
        //Transform bTransform = fInfo.barrelAttachmentTransform;
        //wScript.barrelInstance = Instantiate(barrel,weapon.transform);
        //wScript.barrelInstance.transform.localPosition = bTransform.localPosition;
        //wScript.barrelInstance.transform.localRotation = Quaternion.identity;

        //// Instantiate Clip
        //GameObject clip = weaponArchetypes.clipsList[(int)clipSlot.slotClipAsset.clipType];
        //Transform cTransform = fInfo.clipAttachmentTransform;
        //wScript.clipInstance = Instantiate(clip, weapon.transform);
        //wScript.clipInstance.transform.localPosition = cTransform.localPosition;
        //wScript.clipInstance.transform.localRotation = Quaternion.identity;

        //// Instantiate Trigger
        //GameObject trigger = weaponArchetypes.triggersList[(int)triggerSlot.slotTriggerAsset.triggerType];
        //Transform tTransform = fInfo.triggerAttachmentTransform;
        //wScript.triggerInstance = Instantiate(trigger, weapon.transform);
        //wScript.triggerInstance.transform.localPosition = tTransform.localPosition;
        //wScript.triggerInstance.transform.localRotation = Quaternion.identity;
    }

    public void FrameChange(GameObject f)
    {
        Destroy(wScript.frameInstance);

        wScript.frameInstance = Instantiate(f, weapon.transform);
        wScript.frameInfo = wScript.frameInstance.GetComponent<FrameInfo>();

        // Make sure objects are in the correct position on the new frame (rotations are identity which should already be set)
        wScript.barrelInstance.transform.localPosition = wScript.frameInfo.barrelAttachmentTransform.localPosition;

        wScript.clipInstance.transform.localPosition = wScript.frameInfo.clipAttachmentTransform.localPosition;

        wScript.triggerInstance.transform.localPosition = wScript.frameInfo.triggerAttachmentTransform.localPosition;
    }

    public void BarrelChange(GameObject b)
    {
        Destroy(wScript.barrelInstance);

        Transform bTransform = fInfo.barrelAttachmentTransform;
        wScript.barrelInstance = Instantiate(b, weapon.transform);
        wScript.barrelInstance.transform.localPosition = bTransform.localPosition;
        wScript.barrelInstance.transform.localRotation = Quaternion.identity;
    }

    public void ClipChange(GameObject c)
    {
        Destroy(wScript.clipInstance);

        Transform cTransform = fInfo.clipAttachmentTransform;
        wScript.clipInstance = Instantiate(c, weapon.transform);
        wScript.clipInstance.transform.localPosition = cTransform.localPosition;
        wScript.clipInstance.transform.localRotation = Quaternion.identity;
    }

    public void TriggerChange(GameObject t)
    {
        Destroy(wScript.triggerInstance);

        Transform tTransform = fInfo.triggerAttachmentTransform;
        wScript.triggerInstance = Instantiate(t, weapon.transform);
        wScript.triggerInstance.transform.localPosition = tTransform.localPosition;
        wScript.triggerInstance.transform.localRotation = Quaternion.identity;
    }

    private void DeduceProjectileType()
    {
        // Weapons are currently only affected by barrel type, and then by clip type.
        // TODO - Fix this by using each module.
        uint fireType = 32; // flame bounce
        /*
        uint fireType = (uint)barrelSlot.GetInfusionType();

        fireType <<= (int)clipSlot.GetInfusionType();
        */

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

    public void FrameIconToggle()
    {
        // Expand the UI for the Frame Slot.
        // Activate the FrameSlot script
    }
}