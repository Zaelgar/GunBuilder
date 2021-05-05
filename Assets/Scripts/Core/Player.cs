using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]

public class Player : MonoBehaviour
{
    public Camera playerCamera;
    public Transform weaponPoint;
    public GameObject frame;


    // Weapon Variables
    public Weapon weaponScript;


    [Header("Player Movement Variables")]
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;


    // Private 
    CharacterController characterController;
    MeshRenderer capsuleRenderer;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;


    // Input Events
    public static UnityEvent OnPlayerUseEvent = new UnityEvent();
    float cameraMoveTime = 1.0f;
    Vector3 oldCamPosition;
    Vector3 oldCamEulerAngles;
    UnityAction playerCanMove;


    // Input state variables
    [HideInInspector]
    public bool canMove = true;
    bool canUseInput = true;


    Vector2 movementInput;
    Vector2 lookDelta;
    bool hasJumped = false;
    bool isFiring = false;
    bool isRunning = false;

    private void Awake()
    {
        ServiceLocator.Register<Player>(this);
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        capsuleRenderer = GetComponentInChildren<MeshRenderer>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerCanMove += CanMoveAgain;
    }

    void Update()
    {
        weaponPoint.rotation = playerCamera.transform.rotation;

        if (canUseInput)
        {
            UpdateMovement();
        }
    }

    void UpdateMovement()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * movementInput.y : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * movementInput.x : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (hasJumped && canMove && characterController.isGrounded)
        {
            hasJumped = false;
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -lookDelta.y * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, lookDelta.x * lookSpeed, 0);
        }
    }

    public void CanMoveAgain()
    {
        canUseInput = true;
        SetVisible(true);

        //Debug.Log("Can Move Again Action has Triggered");
    }

    public void CameraLerpToWorkbench(Transform camTransform)
    {
        canUseInput = false;
        oldCamPosition = playerCamera.transform.position;
        oldCamEulerAngles = playerCamera.transform.rotation.eulerAngles;

        LeanTween.move(playerCamera.gameObject, camTransform.position, cameraMoveTime);
        LeanTween.rotate(playerCamera.gameObject, camTransform.rotation.eulerAngles, cameraMoveTime);
        //Debug.Log("Camera is Lerping to the workbench");
    }

    public void CameraLerpToPlayer()
    {
        LeanTween.move(playerCamera.gameObject, oldCamPosition, cameraMoveTime).setOnComplete(CanMoveAgain);
        LeanTween.rotate(playerCamera.gameObject, oldCamEulerAngles, cameraMoveTime);
        //Debug.Log("Camera is Lerping back to player");
    }

    public void SetVisible(bool state)
    {
        capsuleRenderer.enabled = state;
    }


    #region Player Input Callbacks

    public void OnPlayerFire(InputAction.CallbackContext context)
    {
        if (canUseInput)
        {
            if (context.performed)
            {
                isFiring = true;
                Debug.Log("Player has fired");
            }
            else if(context.canceled)
            {
                isFiring = false;
                Debug.Log("Player has stopped firing");
            }

            weaponScript.SetTriggerDown(isFiring);
        }
    }

    public void OnPlayerJump(InputAction.CallbackContext context)
    {
        if (canUseInput)
        {
            if (context.performed)
                hasJumped = true;
        }
    }

    public void OnPlayerLook(InputAction.CallbackContext context)
    {
        if (canUseInput)
        {
            lookDelta = context.ReadValue<Vector2>();
        }
    }

    public void OnPlayerMove(InputAction.CallbackContext context)
    {
        if (canUseInput)
        {
            movementInput = context.ReadValue<Vector2>();
        }
    }

    public void OnPlayerRun(InputAction.CallbackContext context)
    {
        if (canUseInput)
        {
            if (context.started)
                isRunning = true;
            else if (context.performed)
                isRunning = false;
        }
    }

    public void OnPlayerUse(InputAction.CallbackContext context)
    {
        if (canUseInput)
        {
            if (context.performed)
            {
                OnPlayerUseEvent.Invoke();
                //Debug.Log("Player has pressed Use");
            }
        }
    }

    #endregion
}