using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Weapon Modules/New Clip")]
public class Clip : Module
{
    // TODO: Eliminate hard references inside of object (use the Weapon archetypes for objects)
    public GameObject clipObject;

    public float clipScale = 1.0f;

    public override void Awake()
    {
        moduleType = ModuleType.Clip;
    }

    public void InitializeNew
       (
        GameObject obj,
        int mIndex,
        string objName,
        WeaponArchetypes.InfusionType infusion,
        float cScale = 1.0f
        )
    {
        clipObject = obj;
        moduleIndex = mIndex;
        moduleType = ModuleType.Clip;
        name = objName;
        infusionType = infusion;
        clipScale = cScale;
    }
}