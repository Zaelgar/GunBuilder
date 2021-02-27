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
}