using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class FrameSlot : WeaponSlot<Frame>
{
    [Header("Slot Specific Variables")]

    public Slider newFrameScaleSlider;

    public Frame frameAsset;
    public FrameInfo frameInfo;

    public float maxScale = 1.2f;
    public float minScale = 0.9f;

    protected override void Start()
    {
        base.Start();
        moduleAssetPath = weaponArchetypes.frameAssetPath;

        newFrameScaleSlider.maxValue = maxScale;
        newFrameScaleSlider.minValue = minScale;
        newFrameScaleSlider.value = 1.0f;

        PopulateSavedAssetsList();

        InitializeSlotObject();
        PopulateFrameTypes();
    }

    void PopulateFrameTypes()
    {
        newObjectTypeDropdown.ClearOptions();
        List<string> options = new List<string>();

        foreach (GameObject obj in weaponArchetypes.framesList)
        {
            options.Add(obj.name);
        }

        // Should be alphabetical, but relies on WeaponArchetypes
        newObjectTypeDropdown.AddOptions(options);
    }

    private void InitializeSlotObject(Frame f = null) // if null, instantiates the first object in the module list
    {
        if (slotObjectInstance)
            Destroy(slotObjectInstance);

        if (f != null)
        {
            // Frame Object and Prefab
            slotObjectInstance = Instantiate(weaponArchetypes.framesList[f.moduleIndex], transform);
            newObjectTypeDropdown.value = f.moduleIndex;

            // Frame Scale
            newFrameScaleSlider.value = f.frameScale;
            slotObjectInstance.transform.localScale = new Vector3(f.frameScale, f.frameScale, f.frameScale);

            // Infusion Type
            infusionType = f.infusionType;
            newObjectInfusionDropdown.value = (int)f.infusionType;
            slotObjectInstance.GetComponent<ElementMaterial>().ChangeElementalMaterials(weaponArchetypes.infusionModuleMaterials[(int)f.infusionType]);

            // Frame Name
            newObjectInputField.text = f.name;

            // Frame Info Script
            frameInfo = f.frameInfo;
            if (!frameInfo)
                Debug.LogWarning("InitializeSlotObject (Frame) with frame: " + f.name + " has no FrameInfo script!");
        }
        else
        {
            slotObjectInstance = Instantiate(weaponArchetypes.framesList[0], transform);
            newFrameScaleSlider.value = 1.0f;
            slotObjectInstance.transform.localScale = Vector3.one;

            slotObjectInstance.GetComponent<ElementMaterial>().ChangeElementalMaterials(weaponArchetypes.infusionModuleMaterials[0]);

            frameInfo = slotObjectInstance.GetComponent<FrameInfo>();
            if (!frameInfo)
                Debug.LogWarning("InitializeSlotObject (Frame) with weaponArchetypes frame 0 has no FrameInfo!");
        }
    }

    public void OnSavedObjectDropdownChange()
    {
        if (savedObjectDropdown.options.Count < 2 || savedObjectDropdown.value == 0)
        {
            InitializeSlotObject(); // Initialize default if the option is 0 or NONE
            return;
        }

        frameAsset = savedModuleList[savedObjectDropdown.value-1]; // Option 0 is None, so we would never look for an object if 0

        InitializeSlotObject(frameAsset);
    }

    public override void OnNewObjectTypeDropdown()
    {
        Destroy(slotObjectInstance);

        slotObjectInstance = Instantiate(weaponArchetypes.framesList[newObjectTypeDropdown.value]);

        // material infusion
        slotObjectInstance.GetComponent<ElementMaterial>().ChangeElementalMaterials(weaponArchetypes.infusionModuleMaterials[(int)infusionType]);

        // scaling
        slotObjectInstance.transform.localScale = new Vector3(
            newFrameScaleSlider.value,
            newFrameScaleSlider.value,
            newFrameScaleSlider.value
            );
    }

    public void OnNewFrameScaleChange()
    {
        if (slotObjectInstance != null)
        {
            slotObjectInstance.transform.localScale = new Vector3(
                newFrameScaleSlider.value,
                newFrameScaleSlider.value,
                newFrameScaleSlider.value
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

        frameAsset = ScriptableObject.CreateInstance<Frame>();
        frameAsset.InitializeNew(
            weaponArchetypes.framesList[newObjectTypeDropdown.value],
            newObjectTypeDropdown.value,
            newObjectInputField.text,
            infusionType,
            newFrameScaleSlider.value
                );

        moduleSet.AddModule(frameAsset);

        SavedButtonDialogue();
    }

    public Frame GetWeaponFrame()
    {
        Frame f = ScriptableObject.CreateInstance<Frame>();

        string text;
        if (newObjectInputField.text == "")
        {
            text = "UNAMED_FRAME";
        }
        else
        {
            text = newObjectInputField.text;
        }

        f.InitializeNew(
            weaponArchetypes.framesList[newObjectTypeDropdown.value],
            newObjectTypeDropdown.value,
            text,
            infusionType,
            newFrameScaleSlider.value
                );

        return f;
    }
}