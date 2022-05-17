using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Streak : MonoBehaviour{

    [SerializeField] AnimationCurve _curve;
    [SerializeField] Gradient _colorGradient;
    [SerializeField] Image _progress;
    [SerializeField] float _segments = 5;

    float _accumulatedTime = 0;
    float _baseFillAmount = 0;
    bool _showAnimation = false;
    bool _showRestartAnimation = false;
    bool _specialEnemyActive = false;
    
    void Start(){
        ScoreManager.instance.onChangeStreak += SetStreak;
        ScoreManager.instance.onActiveSpecialEnemy += SpecialEnemyPunch;
        _progress.fillAmount = 0;
    }

    void OnDisable() {
        ScoreManager.instance.onChangeStreak -= SetStreak;
        ScoreManager.instance.onActiveSpecialEnemy -= SpecialEnemyPunch;
    }

    void SpecialEnemyPunch(bool active){
        _specialEnemyActive = active;
    }

    void SetStreak(int combo, int streakRest){
        StartCoroutine(SetParameters(combo, streakRest));
    } 
    
    IEnumerator SetParameters(int combo, int streakRest){
        yield return new WaitForEndOfFrame();
        CancelLastAnimation();
        if (!_specialEnemyActive){
            if (combo == 0 && streakRest == 0){
                _accumulatedTime = _progress.fillAmount*_curve.keys[1].time;
                _showRestartAnimation = true;
            }
            else{
                _baseFillAmount = 0;
                if (streakRest>1)
                    _baseFillAmount = (streakRest-1)/_segments;
                if (combo > 0 && streakRest == 0)
                    _baseFillAmount = 0.8f;
                _showAnimation = true;
            }
        }
    }

    void CancelLastAnimation(){
        _accumulatedTime = 0;
        _showAnimation = false;
        _showRestartAnimation = false;
    }

    void Update(){
        if (!_specialEnemyActive){
            if (_showAnimation)
                SetProgress();            

            if (_showRestartAnimation)
                ReturnToZero();
        }else{
            _progress.fillAmount = 1;
            _progress.color = _colorGradient.Evaluate(1);
        }
    }

    void SetProgress(){
        _accumulatedTime+=Time.deltaTime;
        _progress.fillAmount = _baseFillAmount + (_curve.Evaluate(_accumulatedTime)/_segments);
        _progress.color = _colorGradient.Evaluate(_progress.fillAmount);
        if (_accumulatedTime >= _curve.keys[1].time){
            _showAnimation = false; 
            _progress.fillAmount = _baseFillAmount+(1/_segments);
            if (1-_progress.fillAmount<0.05f){
                _showRestartAnimation = true;
                _accumulatedTime = _curve.keys[1].time;
            }
        }
    }

    void ReturnToZero(){
        _accumulatedTime-=Time.deltaTime;
        float curveValue = _curve.Evaluate(_accumulatedTime);
        _progress.fillAmount = (curveValue<=_progress.fillAmount?curveValue:_progress.fillAmount);
        if (_progress.fillAmount<0.05f){
            _showRestartAnimation = false;
            _progress.fillAmount = 0;
        }
    }
}
