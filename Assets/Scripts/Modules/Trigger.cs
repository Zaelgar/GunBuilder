using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Weapon Modules/New Trigger")]
public class Trigger : Module
{
    // TODO: Eliminate hard references inside of object (use the Weapon archetypes for objects)
    public GameObject trigger;

    public enum TriggerType
    {
        SemiAuto,
        BurstFire,
        UnderBarrel,
        NONE,
    }

    public TriggerType triggerType = TriggerType.NONE;

    public override void Awake()
    {
        moduleType = ModuleType.Trigger;
        moduleName = "NEW_TRIGGER";
    }

    public override uint ApplyModule()
    {
        return (uint)infusionType;
    }
}