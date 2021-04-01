using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class WeaponSlot : MonoBehaviour
{
    protected string moduleAssetPath = "";
    protected WeaponArchetypes weaponArchetypes;
    protected GameObject slotObjectInstance;

    #region Common Slot UI References
    public Toggle slotToggle;
    public float animationTime = 0.3f;

    [Header("UI Saved Object Panel UI")]
    public RectTransform slotSavedPanel;
    private bool isSavedPanelOpen = false;
    public TMPro.TMP_Dropdown savedObjectDropdown;
    public Button newObjectButton;

    [Header("UI New Object Panel UI")]
    public RectTransform slotNewPanel;
    private bool isNewPanelOpen = false;
    public TMPro.TMP_InputField newObjectInputField;
    public TMPro.TMP_Dropdown newObjectTypeDropdown;
    public TMPro.TMP_Dropdown newObjectInfusionDropdown;
    public Button newObjectSaveButton;

    [Header("UI No Name Warning Popup")]
    public RectTransform noNamePopupPanel;
    public Button noNameOkayButton;

    #endregion

    #region Common Slot Variables

    public Module.InfusionType infusionType;
    public Module.ModuleType moduleType;
    public string moduleName;

    #endregion

    public virtual void Start()
    {
        weaponArchetypes = ServiceLocator.Get<WeaponArchetypes>();
        if (weaponArchetypes == null)
        {
            Debug.LogError("WeaponArchetypes not found.");
        }
    }

    protected abstract void InitializeDefaultSlot();

    protected abstract void PopulateSavedAssetsList();

    #region UI Functions

    public void OnSlotToggle()
    {
        if(!slotToggle.isOn) // The toggle for this weapon slot is off
        {
            ClosePanel(slotNewPanel);
            isSavedPanelOpen = false;
            ClosePanel(slotSavedPanel);
            isNewPanelOpen = false;
        }
        else // The toggle for this weapon slot is on
        {
            if(!isSavedPanelOpen)
            {
                OpenPanel(slotSavedPanel);
                isSavedPanelOpen = true;
            }
            else
            {
                ClosePanel(slotSavedPanel);
                isSavedPanelOpen = false;
                OpenPanel(slotNewPanel);
                isNewPanelOpen = true;
            }
        }
    }

    public void OnSavedObjectDropdown()
    {

    }

    public void OnNewObjectButtonPress()
    {

    }

    public void OnNewObjectTypeDropdown()
    {

    }

    public void OnNewObjectInfusionChange()
    {

    }

    public void OnNewObjectSaveButtonPress()
    {
        // Check name input field
    }

    #endregion

    #region Panel Animation

    public void OpenPanel(RectTransform rT)
    {
        LeanTween.move(rT, new Vector3(100, -27, 0), animationTime);
    }

    public void ClosePanel(RectTransform rT)
    {
        LeanTween.move(rT, new Vector3(-100, -27, 0), animationTime);
    }

    #endregion
}