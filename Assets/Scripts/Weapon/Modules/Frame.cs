using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Weapon Modules/New Frame")]
public class Frame : Module
{
    public GameObject frameObject;
    public FrameInfo frameInfo;
    public float frameScale = 1.0f;

    public override void Awake()
    {
        moduleType = ModuleType.Frame;
    }

    public FrameInfo GetFrameInfo()
    {
        if (frameInfo == null)
        {
            frameInfo = frameObject.GetComponent<FrameInfo>();
            return frameInfo;
        }
        else
            return frameInfo;
    }

    public void InitializeNew
        (
        GameObject frameObj,
        int mIndex,
        string objName,
        WeaponArchetypes.InfusionType infusion,
        float scale
        )
    {
        frameObject = frameObj;
        moduleIndex = mIndex;
        moduleType = ModuleType.Frame;
        name = objName;
        infusionType = infusion;
        frameScale = scale;
        frameInfo = frameObj.GetComponent<FrameInfo>();
        if (!frameInfo)
            Debug.LogWarning("New saved frame GameObject does not contain a FrameInfo component!");

        moduleType = ModuleType.Frame;
    }
}