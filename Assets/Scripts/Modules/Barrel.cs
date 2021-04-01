using UnityEngine;
using System.Collections;

[System.Serializable]
[CreateAssetMenu(menuName = "Weapon Modules/New Barrel")]
public class Barrel : Module
{
    // TODO: Eliminate hard references inside of object (use the Weapon archetypes for objects)
    public GameObject barrelObject;

    public enum BarrelType
    {
        Carbine,
        Gatling,
        Splitter,
        HighPressureChamber,
        NONE,
    }

    public BarrelType barrelType = BarrelType.NONE;
    public float barrelLength = 1.0f;
    public float barrelScale = 1.0f;

    public override void Awake()
    {

    }

    public void Initialize
       (string name = "NO_NAME_BARREL",
        InfusionType iType = InfusionType.Electric,
        BarrelType bType = BarrelType.Carbine,
        float bLength = 1.0f,
        float bScale = 0.0f)
    {
        moduleType = ModuleType.Barrel;
        moduleName = name;
        infusionType = iType;
        barrelType = bType;
        barrelLength = bLength;
        barrelScale = bScale;
    }

    public override uint ApplyModule()
    {
        return (uint)infusionType;
    }
}