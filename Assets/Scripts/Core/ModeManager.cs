using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeManager : MonoBehaviour
{
    public enum GameMode
    {
        Testing,
        GunBuilding
    }
    public GameMode CurrentGameMode { get; private set; }

    public Canvas workBenchActivator;
    public WorkTable workTableScript;
    public Button testWeaponButton;

    private void Awake()
    {
        ServiceLocator.Register<ModeManager>(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        workTableScript = ServiceLocator.Get<WorkTable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTestButtonPress()
    {
        workBenchActivator.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        workBenchActivator.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        workBenchActivator.enabled = false;
    }
}