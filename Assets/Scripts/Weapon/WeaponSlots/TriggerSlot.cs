using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerSlot : WeaponSlot<Trigger>
{
    [Header("Slot Specific Variables")]
    public Trigger triggerAsset;

    public TMPro.TMP_Text newTriggerBurstCountText;
    public Slider newTriggerBurstCountSlider;

    public int maxBurstCount = 5; // min is 1 (1 shot per shot) max is this #

    protected override void Start()
    {
        base.Start();
        moduleAssetPath = weaponArchetypes.triggerAssetPath;

        PopulateSavedAssetsList();

        newTriggerBurstCountSlider.minValue = 1; // min is 1 (1 shot per shot)
        newTriggerBurstCountSlider.maxValue = maxBurstCount;
        newTriggerBurstCountSlider.value = 1;

        InitializeSlotObject();
        PopulateTriggerTypes();
    }

    void PopulateTriggerTypes()
    {
        newObjectTypeDropdown.ClearOptions();
        List<string> options = new List<string>();

        foreach (GameObject obj in weaponArchetypes.triggersList)
        {
            options.Add(obj.name);
        }

        // Should be alphabetical, but relies on WeaponArchetypes
        newObjectTypeDropdown.AddOptions(options);
    }

    void InitializeSlotObject(Trigger t = null)
    {
        if (slotObjectInstance)
            Destroy(slotObjectInstance);

        if (t != null)
        {
            // Object and Prefab
            slotObjectInstance = Instantiate(weaponArchetypes.triggersList[t.moduleIndex], transform);
            newObjectTypeDropdown.value = t.moduleIndex;

            // Infusion Type
            infusionType = t.infusionType;
            newObjectInfusionDropdown.value = (int)t.infusionType;
            slotObjectInstance.GetComponent<ElementMaterial>().ChangeElementalMaterials(weaponArchetypes.infusionModuleMaterials[(int)t.infusionType]);


            // Burst Fire
            newTriggerBurstCountSlider.value = t.burstCount;

            // Name
            newObjectInputField.text = t.name;
        }
        else
        {
            slotObjectInstance = Instantiate(weaponArchetypes.triggersList[0], transform);
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

        triggerAsset = savedModuleList[savedObjectDropdown.value - 1]; // Option 0 is None, so we would never look for an object if 0

        InitializeSlotObject(triggerAsset);
    }

    public override void OnNewObjectTypeDropdown()
    {
        Destroy(slotObjectInstance);

        slotObjectInstance = Instantiate(weaponArchetypes.triggersList[newObjectTypeDropdown.value], transform);

        // Correct infusion
        slotObjectInstance.GetComponent<ElementMaterial>().ChangeElementalMaterials(weaponArchetypes.infusionModuleMaterials[(int)infusionType]);
    }

    public void OnNewTriggerBurstCountSliderChange()
    {
        newTriggerBurstCountText.text = ((int)newTriggerBurstCountSlider.value).ToString();
    }

    public override void OnNewObjectSaveButtonPress()
    {
        if (newObjectInputField.text == "")
        {
            LeanTween.scale(noNamePopupPanel, Vector3.one, animationTime);
            return;
        }

        triggerAsset = ScriptableObject.CreateInstance<Trigger>();
        triggerAsset.InitializeNew(
            weaponArchetypes.triggersList[newObjectTypeDropdown.value],
            newObjectTypeDropdown.value,
            newObjectInputField.text,
            infusionType,
            (int)newTriggerBurstCountSlider.value
                );

        moduleSet.AddModule(triggerAsset);

        SavedButtonDialogue();
    }

    public Trigger GetWeaponTrigger()
    {
        Trigger t = ScriptableObject.CreateInstance<Trigger>();

        string text;
        if (newObjectInputField.text == "")
        {
            text = "UNAMED_TRIGGER";
        }
        else
        {
            text = newObjectInputField.text;
        }

        t.InitializeNew(
            weaponArchetypes.triggersList[newObjectTypeDropdown.value],
            newObjectTypeDropdown.value,
            text,
            infusionType,
            (int)newTriggerBurstCountSlider.value
                );

        return t;
    }
}