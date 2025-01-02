using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

public class LeftControllerAnim : MonoBehaviour
{
    public GameObject XROrigin;

    [Header("ThumbStick Animation")]
    public float thumbStickAngle = 30f;
    public Transform thumbStick;

    [Header("Trigger Animation")]
    public float triggerButtonAngle = -20f;
    public Transform triggerButton;

    public float moveSpeed;
    public float jumpPower = 5f;
    public bool IsJump { get; set; }
    private Player player;

    private ActionBasedContinuousMoveProvider _moveProvider;
    private ActionBasedController _controller;


    private void Awake()
    {
        _controller = GetComponentInParent<ActionBasedController>();
        XROrigin = GameObject.Find("XR Origin");
        _moveProvider = GameObject.FindObjectOfType<ActionBasedContinuousMoveProvider>();
        _moveProvider.moveSpeed = moveSpeed;

        player = FindObjectOfType<Player>();
        player.leftCtrl = this;
    }

    private void OnEnable()
    {
        _controller.rotateAnchorAction.action.performed += ThumbXAction;
        _controller.rotateAnchorAction.action.canceled += ThumbXAction;
        _controller.translateAnchorAction.action.performed += ThumbYAction;
        _controller.translateAnchorAction.action.canceled += ThumbYAction;

        _controller.activateAction.action.performed += TriggerButtonAction;
    }

    private void OnDisable()
    {
        _controller.rotateAnchorAction.action.performed -= ThumbXAction;
        _controller.rotateAnchorAction.action.canceled -= ThumbXAction;
        _controller.translateAnchorAction.action.performed -= ThumbYAction;
        _controller.translateAnchorAction.action.canceled -= ThumbYAction;

        _controller.activateAction.action.performed -= TriggerButtonAction;
    }

    private void ThumbXAction(Context context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        Vector3 euler = thumbStick.localEulerAngles;
        euler.z = input.x * thumbStickAngle;
        thumbStick.localEulerAngles = euler;
    }

    private void ThumbYAction(Context context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        Vector3 euler = thumbStick.localEulerAngles;
        euler.x = -input.y * thumbStickAngle;
        thumbStick.localEulerAngles = euler;
    }

    private void TriggerButtonAction(Context context)
    {
        if (context.performed && !IsJump)
        {
            IsJump = true;
            player.Jump();
        }
    }
}
