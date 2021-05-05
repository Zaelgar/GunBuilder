using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorkTable : MonoBehaviour
{
    public Canvas weaponEditCanvas;
    public Canvas editWeaponPopupCanvas;

    public Transform cameraTransform;

    public FrameSlot fSlot;
    public BarrelSlot bSlot;
    public ClipSlot cSlot;
    public TriggerSlot tSlot;

    Player playerRef;
    Weapon weaponRef;

    public static UnityEvent OnTestWeaponButton = new UnityEvent();

    private void Awake()
    {
        ServiceLocator.Register<WorkTable>(this);
        editWeaponPopupCanvas.enabled = false;
        weaponEditCanvas.enabled = false;
    }

    private void Start()
    {
        playerRef = ServiceLocator.Get<Player>();
        weaponRef = ServiceLocator.Get<Weapon>();
        Player.OnPlayerUseEvent.AddListener(OnPlayerUse);
    }

    public void OnWeaponTestButton()
    {
        OnTestWeaponButton.Invoke();

        playerRef.CameraLerpToPlayer();

        weaponEditCanvas.enabled = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        weaponRef.CreateNewWeapon(fSlot.GetWeaponFrame(), bSlot.GetWeaponBarrel(), cSlot.GetWeaponClip(), tSlot.GetWeaponTrigger());
    }

    public void OnPlayerUse()
    {
        if(editWeaponPopupCanvas.enabled)// Player is within the use trigger collider
        {
            editWeaponPopupCanvas.enabled = false;
            weaponEditCanvas.enabled = true;

            playerRef.CameraLerpToWorkbench(cameraTransform);
            playerRef.SetVisible(false);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            editWeaponPopupCanvas.enabled = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            editWeaponPopupCanvas.enabled = false;
        }
    }
}