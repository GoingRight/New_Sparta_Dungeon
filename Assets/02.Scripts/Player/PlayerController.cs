using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    private Vector2 curMoveInput;
    public float speed;
    public float walkSpeed;
    public float runSpeed;
    public float jumpPower;
    public int jumpCount;
    public float jumpCost;
    public float runStaminaCost;
    public LayerMask groundLayerMask;
    private bool isRun;
    public bool IsRun
    {
        get { return isRun; }
        set
        {
            isRun = value;
            if (value) speed = runSpeed;
            else
            {
                speed = walkSpeed;
                player.condition.lastStaminaUse = Time.time;
            }
        }
    }
    private bool didUseRun = false;

    [Header("Look")]
    private Vector2 mouseDelta;
    public float maxHeadUp;
    public float maxHeadDown;
    public float lookSencsitivity;
    private float camCurXRot;
    public Transform head;
    public bool canLook;

    private Rigidbody _rb;
    private Player player;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        player = CharacterManager.Instance.Player;
        Cursor.lockState = CursorLockMode.Locked;
        speed = walkSpeed;
        canLook = true;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            Look();
        }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMoveInput.y + transform.right * curMoveInput.x;
        dir *= speed;
        dir.y = _rb.velocity.y;
        _rb.velocity = dir;
    }

    public void Onmove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMoveInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMoveInput = Vector2.zero;
        }
    }

    private void Look()
    {
        camCurXRot += mouseDelta.y * lookSencsitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, maxHeadDown, maxHeadUp);
        head.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSencsitivity, 0);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase != InputActionPhase.Started)
        {
            return;
        }
        if (_rb.velocity.y <=0)
        {
            isGround();
        }

        if (jumpCount != 0 && player.condition.Stamina.curValue > jumpCost)
        {
            player.condition.lastStaminaUse = Time.time;
            player.condition.Stamina.Subtract(jumpCost);
            jumpCount--;
            _rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    void isGround()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position+(transform.forward*0.2f)+ (transform.up*0.01f), Vector3.down),
            new Ray(transform.position+(transform.right*0.2f)+ (transform.up*0.01f), Vector3.down),
            new Ray(transform.position+(-transform.forward*0.2f)+ (transform.up*0.01f), Vector3.down),
            new Ray(transform.position+(-transform.right*0.2f)+ (transform.up*0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.011f, groundLayerMask))
            {
                jumpCount = 2;
            }
        }
    }


    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed
            && player.condition.CheckStamina(runStaminaCost))
        {
            if(curMoveInput != Vector2.zero)
            {
                didUseRun = true;
                runco = StartCoroutine(RunCo());
            }
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            if(didUseRun)
            {
                IsRun = false;
                StopCoroutine(runco);
            }
            didUseRun = false;
        }
    }

    public Coroutine runco;

    public IEnumerator RunCo()
    {
        while (player.condition.CheckStamina(runStaminaCost * Time.deltaTime))
        {
            IsRun = true;
            player.condition.Stamina.Subtract(runStaminaCost * Time.deltaTime);
            player.condition.lastStaminaUse = Time.time;
            yield return null;
        }
        IsRun = false;
        StopCoroutine(runco);
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            UIManager.Instance.IsInventory = !UIManager.Instance.IsInventory;
            ToggleCursor();
        }
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle? CursorLockMode.None: CursorLockMode.Locked;
        canLook = !toggle;
    }
}
