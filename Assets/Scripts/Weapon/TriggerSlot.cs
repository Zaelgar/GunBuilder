using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerSlot : WeaponSlot
{
    [Header("Slot Specific Variables")]
    public Trigger triggerAsset;

    private bool isBurstFire;
    private int numBurstShots;

    protected override void InitializeDefaultSlot()
    {
    }

    protected override void PopulateSavedAssetsList()
    {
    }
}