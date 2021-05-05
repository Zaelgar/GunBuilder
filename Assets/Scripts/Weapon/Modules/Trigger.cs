using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Weapon Modules/New Trigger")]
public class Trigger : Module
{
    // TODO: Eliminate hard references inside of object (use the Weapon archetypes for objects)
    public GameObject triggerObject;

    public int burstCount = 0;

    public override void Awake()
    {
        moduleType = ModuleType.Trigger;
    }

    public void InitializeNew
       (
        GameObject obj,
        int mIndex,
        string objName,
        WeaponArchetypes.InfusionType infusion,
        int bCount
        )
    {
        triggerObject = obj;
        moduleIndex = mIndex;
        moduleType = ModuleType.Trigger;
        name = objName;
        infusionType = infusion;
        burstCount = bCount;
    }
}