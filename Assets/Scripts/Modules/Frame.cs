using UnityEngine;
using System.Collections;

[CreateAssetMenu (menuName = "Weapon Modules/New Frame")]
public class Frame : Module
{
    public enum FrameType
    {
        OneHanded,
        TwoHanded,
        ShoulderMount,
        NONE,
    }

    public FrameType frameType = FrameType.NONE;

    public override void Awake()
    {
        moduleType = ModuleType.Frame;
        moduleName = "NEW_FRAME";
    }

    public override uint ApplyModule()
    {
        return (uint)infusionType;
    }
}