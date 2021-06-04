using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClipSlot : WeaponSlot<Clip>
{
    [Header("Slot Specific Variables")]
    public Slider newClipScaleSlider;

    public Clip clipAsset;

    public float minClipScale = 0.5f;
    public float maxClipScale = 1.5f;

    protected override void Start()
    {
        base.Start();
        moduleAssetPath = weaponArchetypes.clipAssetPath;

        newClipScaleSlider.minValue = minClipScale;
        newClipScaleSlider.maxValue = maxClipScale;
        newClipScaleSlider.value = 1.0f;

        PopulateSavedAssetsList();

        InitializeSlotObject();
        PopulateClipTypes();
    }

    void PopulateClipTypes()
    {
        newObjectTypeDropdown.ClearOptions();
        List<string> options = new List<string>();

        foreach (GameObject obj in weaponArchetypes.clipsList)
        {
            options.Add(obj.name);
        }

        // Should be alphabetical, but relies on WeaponArchetypes
        newObjectTypeDropdown.AddOptions(options);
    }

    private void InitializeSlotObject(Clip c = null)
    {
        if (slotObjectInstance)
            Destroy(slotObjectInstance);

        if (c != null)
        {
            // Object and Prefab
            slotObjectInstance = Instantiate(c.clipObject, transform);
            newObjectTypeDropdown.value = c.moduleIndex;

            // Scale
            newClipScaleSlider.value = c.clipScale;
            slotObjectInstance.transform.localScale = new Vector3(c.clipScale, c.clipScale, c.clipScale);

            // Infusion
            infusionType = c.infusionType;
            newObjectInfusionDropdown.value = (int)c.infusionType;
            slotObjectInstance.GetComponent<ElementMaterial>().ChangeElementalMaterials(weaponArchetypes.infusionModuleMaterials[(int)c.infusionType]);

            // Name
            newObjectInputField.text = c.name;
        }
        else
        {
            slotObjectInstance = Instantiate(weaponArchetypes.clipsList[0], transform);
            newClipScaleSlider.value = 1.0f;
            slotObjectInstance.transform.localScale = Vector3.one;

            slotObjectInstance.GetComponent<ElementMaterial>().ChangeElementalMaterials(weaponArchetypes.infusionModuleMaterials[0]);
        }
    }

    public void OnSavedObjectDropdownChange()
    {
        if (savedObjectDropdown.options.Count < 2 || savedObjectDropdown.value == 0)
        {
            InitializeSlotObject(); // Initialize default if the option is 0 or NONE
            return;
        }

        clipAsset = savedModuleList[savedObjectDropdown.value - 1];

        InitializeSlotObject(clipAsset);
    }

    public override void OnNewObjectTypeDropdown()
    {
        Destroy(slotObjectInstance);

        slotObjectInstance = Instantiate(weaponArchetypes.clipsList[newObjectTypeDropdown.value], transform);

        // Material
        slotObjectInstance.GetComponent<ElementMaterial>().ChangeElementalMaterials(weaponArchetypes.infusionModuleMaterials[(int)infusionType]);

        // Scale
        slotObjectInstance.transform.localScale = new Vector3(
            newClipScaleSlider.value,
            newClipScaleSlider.value,
            newClipScaleSlider.value
            );
    }

    public void OnNewClipScaleSliderChange()
    {
        if (slotObjectInstance)
            slotObjectInstance.transform.localScale = new Vector3(
                newClipScaleSlider.value,
                newClipScaleSlider.value,
                newClipScaleSlider.value
                );
    }

    public override void OnNewObjectSaveButtonPress()
    {
        if (newObjectInputField.text == "")
        {
            LeanTween.scale(noNamePopupPanel, Vector3.one, animationTime);
            return;
        }

        clipAsset = ScriptableObject.CreateInstance<Clip>();
        clipAsset.InitializeNew(
            weaponArchetypes.clipsList[newObjectTypeDropdown.value],
            newObjectTypeDropdown.value,
            newObjectInputField.text,
            infusionType,
            newClipScaleSlider.value
            );

        moduleSet.AddModule(clipAsset);

        SavedButtonDialogue();
    }

    public Clip GetWeaponClip()
    {
        Clip c = ScriptableObject.CreateInstance<Clip>();

        string text;
        if (newObjectInputField.text == "")
        {
            text = "UNAMED_CLIP";
        }
        else
        {
            text = newObjectInputField.text;
        }

        c.InitializeNew(
            weaponArchetypes.clipsList[newObjectTypeDropdown.value],
            newObjectTypeDropdown.value,
            text,
            infusionType,
            newClipScaleSlider.value
                );

        return c;
    }
}