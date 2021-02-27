using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class BarrelSlot : WeaponSlot
{
    // TODO - add functionality to use existing Barrel to drive settings data

    public Barrel barrel;
    public string barrelName = "NO_NAME_BARREL";
    public Barrel.BarrelType barrelType = Barrel.BarrelType.Carbine;
    [Range(0.5f, 1.5f)]
    public float barrelScale = 1.0f;
    [Range(0.0f, 0.5f)]
    public float barrelLength = 0f;

    private void Start()
    {
        moduleAssetPath += "Barrels/";
    }

    public void Change()
    {
        if (weaponRef != null)  // Prevents in-editor checks
        {
            weaponRef.SetBarrel(barrel);
            OnSlotChange();
        }
    }

    public void MakeNewBarrel()
    {
        barrel = Barrel.CreateInstance<Barrel>();
        barrel.Initialize(barrelName, infusionType, barrelType, barrelScale, barrelLength);

        string path = moduleAssetPath + barrelName + ".asset";

        barrel.MultiplyValues(barrelScale);

        barrel = ScriptableObjectUtility.SaveAsset(barrel, path);
        weaponRef.SetBarrel(barrel);
    }

    private void OnValidate()
    {
        // Only works in Play Mode!!
        switch (infusionType)
        {
            case Module.InfusionType.Fire:
                GetComponent<MeshRenderer>().material = weaponArchetypes.infusionMaterials[0];
                break;

            case Module.InfusionType.Electric:
                GetComponent<MeshRenderer>().material = weaponArchetypes.infusionMaterials[1];
                break;

            case Module.InfusionType.Magnet:
                GetComponent<MeshRenderer>().material = weaponArchetypes.infusionMaterials[2];
                break;

            case Module.InfusionType.NONE:
                break;
        }

        Vector3 moduleScaling = Vector3.one * barrelScale;
        moduleScaling.y += barrelLength;

        transform.localScale = moduleScaling;
    }
}