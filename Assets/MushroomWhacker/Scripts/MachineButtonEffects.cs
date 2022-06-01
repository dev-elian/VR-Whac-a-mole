using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineButtonEffects : MonoBehaviour
{
    MachineButton _button;
    [SerializeField] Renderer _rend;

    [Header("Buttons Positions")]
    [SerializeField] Vector3 _initPosition;
    [SerializeField] Vector3 _pressedPosition;

    [Header("Colors")]
    [SerializeField] Color _initColor;
    [SerializeField] Color _pressedColor;

    [Header("Sound")]
    [SerializeField] AudioSource _pressSound;

    void Awake() {
        _button = GetComponent<MachineButton>();
    }

    void OnEnable() {
        _button.onChangeButtonState += SetEffects;
    }

    void OnDisable() {
        _button.onChangeButtonState -= SetEffects;
    }

    void SetEffects(bool buttonState){
        if (buttonState){
            _pressSound.Play();
            transform.localPosition = _pressedPosition;
            _rend.material.color = _pressedColor;
        }else{
            transform.localPosition = _initPosition;
            _rend.material.color = _initColor;
        }
    }
}
