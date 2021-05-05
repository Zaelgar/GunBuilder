using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class BarrelSlot : WeaponSlot<Barrel>
{
    [Header("Slot Specific Variables")]
    public Slider newBarrelScaleSlider;
    public Slider newBarrelLengthSlider;

    public Barrel barrelAsset;

    public float minBarrelScale = 0.7f;
    public float maxBarrelScale = 1.5f;
    public float minBarrelLength = 0.5f;
    public float maxBarrelLength = 1.3f;

    protected override void Start()
    {
        base.Start();
        moduleAssetPath = weaponArchetypes.barrelAssetPath;

        // Set sliders min and max
        newBarrelScaleSlider.maxValue = maxBarrelScale;
        newBarrelScaleSlider.minValue = minBarrelScale;
        newBarrelLengthSlider.maxValue = maxBarrelLength;
        newBarrelLengthSlider.minValue = minBarrelLength;

        newBarrelLengthSlider.value = 1.0f;
        newBarrelScaleSlider.value = 1.0f;

        PopulateSavedAssetsList();

        InitializeSlotObject();
        PopulateBarrelTypes();
    }

    void PopulateBarrelTypes()
    {
        newObjectTypeDropdown.ClearOptions();
        List<string> options = new List<string>();

        foreach (GameObject obj in weaponArchetypes.barrelsList)
        {
            options.Add(obj.name);
        }

        // Should be alphabetical, but relies on WeaponArchetypes
        newObjectTypeDropdown.AddOptions(options);
    }

    private void InitializeSlotObject(Barrel b = null)
    {
        if (slotObjectInstance)
            Destroy(slotObjectInstance);

        if (b != null)
        {
            // Object and Prefab
            slotObjectInstance = Instantiate(b.barrelObject, transform);
            newObjectTypeDropdown.value = b.moduleIndex;

            // Scale
            newBarrelScaleSlider.value = b.barrelScale;
            newBarrelLengthSlider.value = b.barrelLength;
            slotObjectInstance.transform.localScale = new Vector3(b.barrelScale, b.barrelScale, b.barrelLength);

            // Infusion
            infusionType = b.infusionType;
            newObjectInfusionDropdown.value = (int)b.infusionType;
            slotObjectInstance.GetComponent<Renderer>().material = weaponArchetypes.infusionMaterials[(int)b.infusionType];

            // Name
            newObjectInputField.text = b.name;
        }
        else
        {
            slotObjectInstance = Instantiate(weaponArchetypes.barrelsList[0], transform);
            newBarrelScaleSlider.value = 1.0f;
            newBarrelLengthSlider.value = 1.0f;
            slotObjectInstance.transform.localScale = Vector3.one; // length and scale are the same here (1.0f)

            slotObjectInstance.GetComponent<Renderer>().material = weaponArchetypes.infusionMaterials[0];
        }
    }

    public void OnSavedObjectDropdownChange()
    {
        if (savedObjectDropdown.options.Count < 2 || savedObjectDropdown.value == 0)
        {
            InitializeSlotObject();
            return;
        }

        barrelAsset = savedModuleList[savedObjectDropdown.value - 1]; // Option 0 is none

        InitializeSlotObject(barrelAsset);
    }

    public override void OnNewObjectTypeDropdown()
    {
        if (slotObjectInstance)
            Destroy(slotObjectInstance);

        // Set infusion and material
        slotObjectInstance = Instantiate(weaponArchetypes.barrelsList[newObjectTypeDropdown.value], transform);
        slotObjectInstance.GetComponent<Renderer>().material = weaponArchetypes.infusionMaterials[(int)infusionType];

        // Set scaling
        slotObjectInstance.transform.localScale = new Vector3(
            newBarrelScaleSlider.value,
            newBarrelScaleSlider.value,
            newBarrelLengthSlider.value
            );
    }

    public void OnNewBarrelScaleSliderChange()
    {
        if (slotObjectInstance)
        {
            slotObjectInstance.transform.localScale = new Vector3(
                newBarrelScaleSlider.value,
                newBarrelScaleSlider.value,
                slotObjectInstance.transform.localScale.z
                );
        }
    }

    public void OnNewBarrelLengthSliderChange()
    {
        if (slotObjectInstance)
        {
            slotObjectInstance.transform.localScale = new Vector3(
                slotObjectInstance.transform.localScale.x,
                slotObjectInstance.transform.localScale.y,
                newBarrelLengthSlider.value
                );
        }
    }

    public override void OnNewObjectSaveButtonPress()
    {
        if (newObjectInputField.text == "")
        {
            LeanTween.scale(noNamePopupPanel, Vector3.one, animationTime);
            return;
        }

        barrelAsset = ScriptableObject.CreateInstance<Barrel>();
        barrelAsset.InitializeNew
            (
            weaponArchetypes.barrelsList[newObjectTypeDropdown.value],
            newObjectTypeDropdown.value,
            newObjectInputField.text,
            infusionType,
            newBarrelLengthSlider.value,
            newBarrelScaleSlider.value
            );

        moduleSet.AddModule(barrelAsset);

        SavedButtonDialogue();
    }

    public Barrel GetWeaponBarrel()
    {
        Barrel b = ScriptableObject.CreateInstance<Barrel>();

        string text;
        if (newObjectInputField.text == "")
        {
            text = "UNAMED_BARREL";
        }
        else
        {
            text = newObjectInputField.text;
        }

        b.InitializeNew(
            weaponArchetypes.barrelsList[newObjectTypeDropdown.value],
            newObjectTypeDropdown.value,
            text,
            infusionType,
            newBarrelLengthSlider.value,
            newBarrelScaleSlider.value
                );

        return b;
    }
}