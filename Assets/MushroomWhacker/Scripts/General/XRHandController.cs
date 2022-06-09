using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class XRHandController : MonoBehaviour
{
    [Header("Trigger")]
    [SerializeField] InputAction _trigger;
    [Header("Grip")]
    [SerializeField] InputAction _grip;
    [Header("Primary")]
    [SerializeField] InputAction _primaryTouch;
    [Header("Secondary")]
    [SerializeField] InputAction _secondaryTouch;

    Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
        _primaryTouch.performed += SetThumb;
        _secondaryTouch.performed += SetThumb;
        _primaryTouch.canceled += UnsetThumb;
        _secondaryTouch.canceled += UnsetThumb;
    }

    void OnEnable() {
        _trigger.Enable();
        _grip.Enable();
        _primaryTouch.Enable();
        _secondaryTouch.Enable();
    }

    void OnDisable() {
        _trigger.Disable();
        _grip.Disable();
        _primaryTouch.Disable();
        _secondaryTouch.Disable();
    }

    void Update(){
        AnimateHand();
    }

    void AnimateHand(){
        SetIndex(_trigger.ReadValue<float>());
        SetThree(_grip.ReadValue<float>());
    }

    void SetIndex(float indexValue){
        animator.SetFloat("_index", indexValue);
    }

    void SetThree(float threeFingersValue){
        animator.SetFloat("_threeFingers", threeFingersValue);
    }

    void SetThumb(InputAction.CallbackContext context){
        animator.SetFloat("_thumb", 1);
    }

    void UnsetThumb(InputAction.CallbackContext context){
        animator.SetFloat("_thumb", 0);
    }
}