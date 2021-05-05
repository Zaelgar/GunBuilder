using UnityEngine;
using System;
using System.Collections;

public abstract class Module : ScriptableObject
{
    public enum ModuleType
    {
        Frame,
        Barrel,
        Clip,
        Trigger,
        NONE,
    }

    #region Variables

    public ModuleType moduleType = ModuleType.NONE;
    public WeaponArchetypes.InfusionType infusionType = 0;
    public int moduleIndex = 0;

    public float addAccuracy = 0.0f;
    public float multAccuracy = 1.0f;

    public float addRecoil = 0.0f;
    public float multRecoil = 1.0f;

    public float addDamage = 0.0f;
    public float multDamage = 1.0f;

    public float addFireRate = 0.0f;
    public float multFireRate = 1.0f;

    public int addClipSize = 0;
    public float multClipSize = 1.0f;

    public int addAmmoCarry = 0;
    public float multAmmoCarry = 1.0f;

    #endregion

    public abstract void Awake();

    public void MultiplyValues(float value)
    {
        multAccuracy *= value;
        multRecoil *= value;
        multDamage *= value;
        multFireRate *= value;
        multClipSize *= value;
        multAmmoCarry *= value;
    }
}