using UnityEngine;
using System.Collections;

[System.Serializable]
[CreateAssetMenu(menuName = "Weapon Modules/New Barrel")]
public class Barrel : Module
{
    public GameObject barrelObject;
    public float barrelLength = 1.0f;
    public float barrelScale = 1.0f;

    public override void Awake()
    {
        moduleType = ModuleType.Barrel;
    }

    public void InitializeNew
       (
        GameObject obj,
        int mIndex,
        string objName,
        WeaponArchetypes.InfusionType infusion,
        float bLength = 1.0f,
        float bScale = 1.0f
        )
    {
        barrelObject = obj;
        moduleIndex = mIndex;
        moduleType = ModuleType.Barrel;
        name = objName;
        infusionType = infusion;
        barrelLength = bLength;
        barrelScale = bScale;
    }
}