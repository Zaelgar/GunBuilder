using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class BarrelSlot : WeaponSlot
{
    //public Material defaultMat;
    //public Material fireMat;
    //public Material magnetMat;
    //public Material electricMat;

    QOLStorage qStore;
    Weapon weaponRef;

    public Barrel barrel;
    public string barrelName = "NO_NAME_BARREL";
    public Module.InfusionType infusionType = Module.InfusionType.Fire;
    public Barrel.BarrelType barrelType = Barrel.BarrelType.Carbine;
    [Range(0.5f, 2.5f)]
    public float barrelScale = 1.0f;
    [Range(0.0f, 2.0f)]
    public float barrelLength = 0f;

    // This was helping me in editor. It will be removed.
    public Vector3 defaultLocalScale = new Vector3(0.12f, 0.6f, 0.12f);

    private void Start()
    {
        qStore = ServiceLocator.Get<QOLStorage>();
        if(qStore == null)
        {
            Debug.LogError("QOLStorage not found. Did you remember to register it with the ServiceLocator?");
        }

        defaultLocalScale = transform.localScale;
        moduleAssetPath = "Assets/Scripts/Module Assets/Barrels/";

        weaponRef = ServiceLocator.Get<Weapon>();
        if(weaponRef == null)
        {
            Debug.LogError("Fatal Error! Weapon reference not found!");
        }
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
        // TODO - add functionality to use existing Barrel to drive settings data

        // Only works in Play Mode!!
        switch (infusionType)
        {
            case Module.InfusionType.Fire:
                //GetComponent<MeshRenderer>().material = qStore.infusionMaterials[(int)infusionType];
                GetComponent<MeshRenderer>().material = qStore.infusionMaterials[0];
                //GetComponent<MeshRenderer>().material = fireMat;
                break;

            case Module.InfusionType.Magnet:
                //GetComponent<MeshRenderer>().material = qStore.infusionMaterials[(int)infusionType];
                GetComponent<MeshRenderer>().material = qStore.infusionMaterials[1];
                //GetComponent<MeshRenderer>().material = magnetMat;
                break;

            case Module.InfusionType.Electric:
                //GetComponent<MeshRenderer>().material = qStore.infusionMaterials[(int)infusionType];
                GetComponent<MeshRenderer>().material = qStore.infusionMaterials[2];
                //GetComponent<MeshRenderer>().material = electricMat;
                break;

            case Module.InfusionType.NONE:
                //GetComponent<MeshRenderer>().material = defaultMat;
                break;
        }

        Vector3 moduleScaling = defaultLocalScale * barrelScale;
        moduleScaling.y += barrelLength;

        transform.localScale = moduleScaling;
    }
}