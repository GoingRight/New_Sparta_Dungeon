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
    public float runStaminaCost;
    public bool isRun;

    [Header("Look")]
    private Vector2 mouseDelta;
    public float maxHeadUp;
    public float maxHeadDown;
    public float lookSencsitivity;
    private float camCurXRot;
    public Transform head;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        Look();
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

    public void OnRun(InputAction.CallbackContext context)
    {
        //if(context.phase == InputActionPhase.Performed &&CheckStamina(runStaminaCost, )
        //{

        //}
    }


}
