using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor;

public abstract class WeaponSlot<T> : MonoBehaviour
{
    #region Common Slot UI References
    public Toggle slotToggle;
    public float animationTime = 0.3f;

    [Header("UI Saved Object Panel")]
    public RectTransform slotSavedPanel;
    private bool isSavedPanelOpen = false;
    public TMPro.TMP_Dropdown savedObjectDropdown;

    [Header("UI New Object Panel")]
    public RectTransform slotNewPanel;
    private bool isNewPanelOpen = false;
    public TMPro.TMP_InputField newObjectInputField;
    public TMPro.TMP_Dropdown newObjectTypeDropdown;
    public TMPro.TMP_Dropdown newObjectInfusionDropdown;

    [Header("UI No Name Warning Popup")]
    public RectTransform noNamePopupPanel;

    [Header("UI Save Button Diologue Popup")]
    public RectTransform saveDialoguePanel;
    #endregion

    protected string moduleAssetPath;
    protected WeaponArchetypes weaponArchetypes;
    protected GameObject slotObjectInstance;
    public ModuleSet moduleSet;

    protected List<T> savedModuleList = new List<T>();
    protected T moduleAsset;

    #region Common Slot Variables
    [Header("Slot Object Variables")]
    public WeaponArchetypes.InfusionType infusionType = 0;
    public Module.ModuleType moduleType;
    public string moduleName;
    #endregion

    protected virtual void Start()
    {
        weaponArchetypes = ServiceLocator.Get<WeaponArchetypes>();
        if (weaponArchetypes == null)
        {
            Debug.LogError("WeaponArchetypes not found.");
        }

        moduleAssetPath = weaponArchetypes.moduleAssetPath;

        PopulateInfusionTypes();

        WorkTable.OnTestWeaponButton.AddListener(CloseAllPanels);
    }

    protected virtual void PopulateSavedAssetsList()
    {
        savedObjectDropdown.ClearOptions();

        UnityEngine.Object[] savedObjects = AssetDatabase.LoadAllAssetsAtPath(moduleAssetPath);
        //Debug.Log("Found " + moduleSet.itemCount + " saved user " + typeof(T) + "(s) in the .asset file.");

        List<string> moduleNames = new List<string>();
        moduleNames.Add("None");
        foreach (var obj in savedObjects)
        {
            if (obj.GetType() == typeof(ModuleSet))
                continue;
            savedModuleList.Add( (T)Convert.ChangeType(obj, typeof(T)) );
            moduleNames.Add(obj.name);
        }

        if (moduleNames.Count != 0)
            savedObjectDropdown.AddOptions(moduleNames);
    }

    public void PopulateInfusionTypes()
    {
        newObjectInfusionDropdown.ClearOptions();
        List<string> options = new List<string>();

        foreach (string s in weaponArchetypes.infusionTypes)
        {
            options.Add(s);
        }

        newObjectInfusionDropdown.AddOptions(options);
    }

    #region UI Functions

    public void OnSlotToggle()
    {
        if (!slotToggle.isOn)
        {
            ClosePanel(slotNewPanel);
            isSavedPanelOpen = false;
            ClosePanel(slotSavedPanel);
            isNewPanelOpen = false;
        }
        else
        {
            if (!isSavedPanelOpen)
            {
                OpenPanel(slotSavedPanel);
                isSavedPanelOpen = true;
                if (isNewPanelOpen)
                {
                    ClosePanel(slotNewPanel);
                    isNewPanelOpen = false;
                }
            }
        }
    }

    public void OnNewObjectButtonPress()
    {
        ClosePanel(slotSavedPanel);
        isSavedPanelOpen = false;
        OpenPanel(slotNewPanel);
        isNewPanelOpen = true;
    }

    public abstract void OnNewObjectTypeDropdown();

    public void OnNewObjectInfusionChange()
    {
        infusionType = (WeaponArchetypes.InfusionType)newObjectInfusionDropdown.value;

        slotObjectInstance.GetComponent<ElementMaterial>().ChangeElementalMaterials(weaponArchetypes.infusionModuleMaterials[newObjectInfusionDropdown.value]);
    }

    public abstract void OnNewObjectSaveButtonPress();

    public void OnNoNamePanelButtonPress()
    {
        LeanTween.scale(noNamePopupPanel, Vector3.zero, animationTime);
    }

    public void SavedButtonDialogue()
    {
        LeanTween.scale(saveDialoguePanel, Vector3.one, animationTime);
    }

    public void OnSaveDialogueButtonPress()
    {
        LeanTween.scale(saveDialoguePanel, Vector3.zero, animationTime);
    }

    #endregion

    #region Panel Animation

    public void CloseAllPanels()
    {
        isNewPanelOpen = false;
        isSavedPanelOpen = false;
        ClosePanel(slotSavedPanel);
        ClosePanel(slotNewPanel);
    }

    public void OpenPanel(RectTransform rT)
    {
        LeanTween.move(rT, new Vector3(0, 0, 0), animationTime);
    }

    public void ClosePanel(RectTransform rT)
    {
        LeanTween.move(rT, new Vector3(-201, 0, 0), animationTime);
    }

    #endregion
}