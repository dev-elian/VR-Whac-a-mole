using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Side{
    Center = 0,
    Left = 1,
    Right = 2
}
public class PlayerControllers : MonoBehaviour
{
    public static PlayerControllers instance;
    const float JOYSTICK_THRESHOLD = 0.5f;

    [Header("Selections")]
    [SerializeField] InputAction _horizontalSelection;
    public Action<Side> onSelect;

    [Header("Accept")]
    [SerializeField] InputAction _accept;
    public Action onAccept;

    [Header("Show Selection")]
    [SerializeField] InputAction _showSelection;
    public Action<bool> onShowSelection;

    void Awake() {
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 

        _horizontalSelection.performed += Select;
        _horizontalSelection.canceled += Select;

        _accept.performed += Accept;
        _accept.canceled += Accept;

        _showSelection.performed += ShowSelection;
        _showSelection.canceled += ShowSelection;
    }

    void OnEnable() {
        _horizontalSelection.Enable();
        _accept.Enable();
        _showSelection.Enable();
    }

    void OnDisable() {
        _horizontalSelection.Disable();
        _accept.Disable();
        _showSelection.Disable();
    }

    void Select(InputAction.CallbackContext context){
        if (onSelect != null){
            Side newSide = Side.Center;
            if (context.ReadValue<float>()>0.5f)
                newSide = Side.Right;
            else{
                if (context.ReadValue<float>()<-0.5f)
                    newSide = Side.Left;
            }
            onSelect(newSide);
        }
    }

    void Accept(InputAction.CallbackContext context){
        if (onAccept != null)
            onAccept();
    }

    void ShowSelection(InputAction.CallbackContext context){
        if (onShowSelection != null)
            onShowSelection(context.ReadValue<float>()>JOYSTICK_THRESHOLD);
    }
}
