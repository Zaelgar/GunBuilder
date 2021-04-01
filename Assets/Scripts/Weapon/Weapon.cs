using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*

Weapon Attributes:

- Accuracy (default radius for shooting cone)
- Recoil (the amount of expansion per shot of the accuracy shooting cone)
- Damage (the amount of damage that a single shot will do)
- Clip Size (the amount of ammo inside of one clip)
- Ammo Capacity (the amount of bullets the player can carry for a weapon)

*/

public class Weapon : MonoBehaviour
{
    public GameObject projectile;

    #region Weapon References
    public GameObject frameInstance;
    // TODO remove frameInfo (not needed I think, weapon is built already)
    public FrameInfo frameInfo;

    public GameObject barrelInstance;

    public GameObject clipInstance;

    public GameObject triggerInstance;
    #endregion

    private void Awake()
    {
        ServiceLocator.Register<Weapon>(this);
    }

    public void InitializeWeapon()
    {

    }

    public void FireWeapon()
    {

    }
}