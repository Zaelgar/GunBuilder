using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameSlot : WeaponSlot
{
    [Header("Slot Specific Variables")]
    public Slider newFrameScaleSlider;

    public Frame frameAsset;
    public FrameInfo frameInfo;

    private float frameScale;

    protected override void InitializeDefaultSlot()
    {
    }

    protected override void PopulateSavedAssetsList()
    {
    }

    public void OnNewFrameScaleChange()
    {

    }
}