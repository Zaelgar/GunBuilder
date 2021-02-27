using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class WeaponSlot : MonoBehaviour
{
    public static Action OnSlotChange;

    // Each slot will have a place that they save their assets to.
    protected string moduleAssetPath = "";

    protected WeaponArchetypes weaponArchetypes;
    protected Weapon weaponRef;

    public Module.InfusionType infusionType = Module.InfusionType.NONE;

    private void Start()
    {
        weaponArchetypes = ServiceLocator.Get<WeaponArchetypes>();
        if (weaponArchetypes == null)
        {
            Debug.LogError("WeaponArchetypes not found. Did you remember to register it with the ServiceLocator?");
        }

        weaponRef = ServiceLocator.Get<Weapon>();
        if (weaponRef == null)
        {
            Debug.LogError("Fatal Error! Weapon reference not found!");
        }
    }

    public void SlotChange()
    {
        // Simplification of null check
        OnSlotChange?.Invoke();
    }
}