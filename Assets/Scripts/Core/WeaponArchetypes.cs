using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Weapon Archetypes")]
public class WeaponArchetypes : ScriptableObject
{
    public List<Material> infusionMaterials;

    // Weapon mesh pieces
    public List<GameObject> framesList;
    public List<GameObject> barrelsList;
    public List<GameObject> clipsList;
    public List<GameObject> triggersList;

    public string moduleAssetPath = "Assets/Scripts/Module Assets/";

    private void OnEnable()
    {
        // Should start as soon as scene is up
        ServiceLocator.Register<WeaponArchetypes>(this);
    }

    private void OnDisable()
    {
        ServiceLocator.Deregister<WeaponArchetypes>();
    }

    private void OnDestroy()
    {
        ServiceLocator.Deregister<WeaponArchetypes>();
    }

    public int ElementToIndex(Module.InfusionType type)
    {
        // Not sure if we need this yet, supposed to be for UI helper
        switch (type)
        {
            case Module.InfusionType.Electric:
                return 0;
            case Module.InfusionType.Fire:
                return 1;
            case Module.InfusionType.Magnet:
                return 2;
            case Module.InfusionType.NONE:
                return 3;
            default:
                return -1;
        }
    }

    public Module.InfusionType IndexToElement(int index)    // Weird, but keeps code in one place in case of changes.
    {
        // For use with UI dropdown OnDropDownChange. Value is from 0-(numElements-1).
        // Infusion Enum is bit shifted layermask values, so we need a conversion
        switch (index)
        {
            case 0:
                return Module.InfusionType.Electric;
            case 1:
                return Module.InfusionType.Fire;
            case 2:
                return Module.InfusionType.Magnet;
            default:
                return Module.InfusionType.NONE;
        }
    }
}