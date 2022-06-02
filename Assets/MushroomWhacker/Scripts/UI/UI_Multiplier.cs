using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Multiplier : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI _tmpro;
    [SerializeField] AnimationCurve _curveAddSize;
    [SerializeField] float _sizeMultiplier;
    [SerializeField] Gradient _colorGradient;
    
    bool _showAnimation = false;
    float _accumulatedTime = 0;
    float _lastCombo =0;

    void Start(){
        ScoreManager.instance.onChangeStreak += SetStreak;
        SetText(1);
        _lastCombo = 1;
    }

    void OnDisable() {
        ScoreManager.instance.onChangeStreak -= SetStreak;
    }

    void SetStreak(int combo, int streakRest){
        if (_lastCombo != combo){
            _lastCombo = combo;
            _accumulatedTime = 0;
            _showAnimation = true;
            SetText(combo);
        }
    } 

    void SetText(int combo){
        _tmpro.text = "x"+(combo);
        float _gradientPosition = (float)combo/ScoreManager.instance.maxComboPossible;
        _gradientPosition = (_gradientPosition>1?1:_gradientPosition);
        _tmpro.outlineColor = _colorGradient.Evaluate(_gradientPosition);
    }

    void Update() {
        if (_showAnimation){
            _accumulatedTime+=Time.deltaTime;
            float sizeToIncrease = (_curveAddSize.Evaluate(_accumulatedTime)*_sizeMultiplier);
            transform.localScale = Vector3.one+new Vector3(sizeToIncrease, sizeToIncrease, sizeToIncrease);
            if (_accumulatedTime >= _curveAddSize.keys[_curveAddSize.keys.Length-1].time){
                _showAnimation = false; 
            }
        }
    }
    
}
