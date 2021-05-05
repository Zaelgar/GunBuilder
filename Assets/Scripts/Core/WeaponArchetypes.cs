using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Weapon Archetypes")]
public class WeaponArchetypes : ScriptableObject
{
    public enum InfusionType
    {
        None,
        Electric,
        Fire,
        Magnet,
    }
    public List<string> infusionTypes;

    public List<Material> infusionMaterials = new List<Material>();

    // Weapon mesh pieces
    public List<GameObject> framesList;
    public List<GameObject> barrelsList;
    public List<GameObject> clipsList;
    public List<GameObject> triggersList;
    public List<GameObject> projectilesList;

    public string frameAssetPath = "Assets/Scripts/UserMadeObjects/UserFrames.asset";
    public string barrelAssetPath = "Assets/Scripts/UserMadeObjects/UserBarrels.asset";
    public string clipAssetPath = "Assets/Scripts/UserMadeObjects/UserClips.asset";
    public string triggerAssetPath = "Assets/Scripts/UserMadeObjects/UserTriggers.asset";

    public string moduleAssetPath = "Assets/Scripts/Module Assets/";

    public Material GetMaterialForInfusion(InfusionType infusionType)
    {
        return infusionMaterials[(int)infusionType];
    }

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
}