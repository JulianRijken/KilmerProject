using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Laurence : MonoBehaviour
{
    public delegate void PerformMove(int deviceID, Vector2 direction);

    public event PerformMove OnMovePerformed;


    [SerializeField] private InputActionAsset inputActionAsset;

    private InputActionMap inputActionMap;
    private InputAction move;

    void Start()
    {
        UnityEngine.InputSystem.Utilities.ReadOnlyArray<Gamepad> gamepads = Gamepad.all;
        for (int i = 0; i < gamepads.Count; i++)
        {
            Debug.Log(gamepads[i].device.id);
        }
        inputActionMap = inputActionAsset.GetActionMap("Game");
        move = inputActionMap.GetAction("Movement");
        //OnMovePerformed += HandleOnMovePerformed;
        move.performed += HandleMovePerformed;
        move.canceled += HandleMoveCanceled;
        move.Enable();
    }

    private void HandleMoveCanceled(InputAction.CallbackContext obj)
    {
        Debug.Log(obj.control.device.id + " " + obj.ReadValue<Vector2>());
    }

    private void HandleMovePerformed(InputAction.CallbackContext obj)
    {
        Debug.Log(obj.control.device.id + " " + obj.ReadValue<Vector2>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
