using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IElementalMaterial
{
    List<Renderer> ElementalRenderers { get; set; }

    public void ChangeElementalMaterials(WeaponArchetypes.InfusionType infusionType);
}