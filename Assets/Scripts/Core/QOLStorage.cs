using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "QOL Data Storage")]
public class QOLStorage : ScriptableObject
{
    public List<Material> infusionMaterials;

    private void OnEnable()
    {
        // Should start as soon as scene is up
        ServiceLocator.Register<QOLStorage>(this);
    }
}