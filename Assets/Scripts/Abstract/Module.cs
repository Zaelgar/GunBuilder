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

    public enum InfusionType
    {
        Fire = 0x1 << 1,
        Magnet = 0x1 << 2,
        Electric = 0x1 << 4,
        NONE,
    }

    public string moduleName = "New Module";
    public ModuleType moduleType = ModuleType.NONE;
    public InfusionType infusionType = InfusionType.NONE;

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

    public abstract void Awake();
    public abstract uint ApplyModule();

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