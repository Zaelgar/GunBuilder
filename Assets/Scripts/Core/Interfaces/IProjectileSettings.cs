using UnityEngine;

public interface IProjectileSettings
{
    Vector3 LaunchDirection { get; set; }
    WeaponArchetypes.InfusionType InfusionType { get; set; }
    public void Launch();
}