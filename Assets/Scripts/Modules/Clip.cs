﻿using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Weapon Modules/New Clip")]
public class Clip : Module
{
    public enum ClipType
    {
        Magazine,
        Drum,
        Tank,
        NONE,
    }

    public ClipType clipType = ClipType.NONE;

    public override void Awake()
    {
        moduleType = ModuleType.Clip;
        moduleName = "NEW_CLIP";
    }

    public override uint ApplyModule()
    {
        return (uint)infusionType;
    }
}