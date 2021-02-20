using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class WeaponSlot : MonoBehaviour
{
    public static Action OnSlotChange;
    protected string moduleAssetPath = "";

    public void SlotChange()
    {
        // Simplification of null check
        OnSlotChange?.Invoke();
    }
}