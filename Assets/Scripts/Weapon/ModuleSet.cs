using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "New User Module Set")]
public class ModuleSet : ScriptableObject
{
    public int itemCount = 0;
    public Module.ModuleType type = Module.ModuleType.NONE;
    public Dictionary<string, Module> modules = new Dictionary<string, Module>();

    public void RemoveAllAssets()
    {
        foreach(var obj in modules)
        {
            modules.Remove(obj.Key);
            AssetDatabase.RemoveObjectFromAsset(obj.Value);
            itemCount--;
        }
    }

    public bool AddModule(Module obj)
    {
        if(!modules.ContainsKey(obj.name))
        {
            modules.Add(name, obj);
            itemCount++;
            AssetDatabase.AddObjectToAsset(obj, this);
            return true;
        }
        else
        {
            Debug.LogWarning("Name already exists. Use a different name to save this module.");
            return false;
        }
    }
}