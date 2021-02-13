using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Weapon Modules/New Trigger")]
public class Trigger : Module
{
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