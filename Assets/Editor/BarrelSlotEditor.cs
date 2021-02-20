using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System;

[CustomEditor(typeof(BarrelSlot))]
public class BarrelSlotEditor : Editor
{
    public static Action<Barrel> OnMakeNewBarrel;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Make new barrel from current settings"))
        {
            BarrelSlot bScript = target as BarrelSlot;

            bScript.MakeNewBarrel();
        }
    }
}