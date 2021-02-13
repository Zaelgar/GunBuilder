using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Weapon Modules/New Barrel")]
public class Barrel : Module
{
    public enum BarrelType
    {
        Carbine,
        Gatling,
        Splitter,
        HighPressureChamber,
        NONE,
    }

    public BarrelType barrelType = BarrelType.NONE;

    public override void Awake()
    {
        moduleType = ModuleType.Barrel;
        moduleName = "NEW_BARREL";
    }

    public override uint ApplyModule()
    {
        return (uint)infusionType;
    }
}