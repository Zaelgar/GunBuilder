using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClipSlot : WeaponSlot
{
    [Header("Slot Specific Variables")]
    public Slider newClipScaleSlider;

    public Clip clipAsset;

    private float clipScale;

    protected override void InitializeDefaultSlot()
    {
    }

    protected override void PopulateSavedAssetsList()
    {
    }

    public void OnNewClipScaleSliderChange()
    {

    }
}