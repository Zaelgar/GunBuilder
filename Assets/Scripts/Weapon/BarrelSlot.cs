using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class BarrelSlot : WeaponSlot
{
    [Header("Slot Specific Variables")]
    public Slider newBarrelScaleSlider;
    public Slider newBarrelLengthSlider;

    public Barrel barrelAsset;

    private float barrelScale;
    private float barrelLength;

    protected override void InitializeDefaultSlot()
    {
    }

    protected override void PopulateSavedAssetsList()
    {
    }

    public void OnNewBarrelScaleSliderChange()
    {

    }

    public void OnNewBarrelLengthSliderChange()
    {

    }
}