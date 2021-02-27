using UnityEngine;
using System.Collections;

[CreateAssetMenu (menuName = "Weapon Modules/New Frame")]
public class Frame : Module
{
    // TODO: Eliminate hard references inside of object (use the Weapon archetypes for objects)
    public GameObject frame;
    public FrameInfo frameInfo;

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

    public FrameInfo GetFrameInfo()
    {
        if (frameInfo == null)
        {
            frameInfo = frame.GetComponent<FrameInfo>();
            return frameInfo;
        }
        else
            return frameInfo;
    }
}