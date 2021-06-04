using System.Collections.Generic;
using UnityEngine;

public class ElementMaterial : MonoBehaviour
{
    public List<Renderer> elementMaterialObjects;

    public void ChangeElementalMaterials(Material mat)
    {
        foreach (Renderer r in elementMaterialObjects)
        {
            r.material = mat;
        }
    }
}