using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ChangeColorScore : MonoBehaviour
{
    [SerializeField] AnimationCurve _curve;
    [SerializeField] Gradient _colors;
    [SerializeField] MeshRenderer _rend;
    bool _change=false;
    float _accumulatedTime=0;

    UI_Score _scoreUI;
    void Awake() {
        _scoreUI = GetComponentInParent<UI_Score>();
    }

    void Start() {
        _scoreUI.onBadPunch += ChangeColor;
    }

    void OnDisable() {
        _scoreUI.onBadPunch -= ChangeColor;
    }

    void ChangeColor(){
        _change = true;
        _accumulatedTime = 0;
    }

    void Update(){
        if (_change){
            _accumulatedTime+=Time.deltaTime;
            _rend.material.color = _colors.Evaluate(_curve.Evaluate(_accumulatedTime));
            if (_accumulatedTime>_curve.keys[_curve.keys.Length-1].time)
                _change = false;
        }
    }
}
