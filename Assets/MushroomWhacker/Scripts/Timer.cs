using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    [Range(25,999)]
    [SerializeField] int _gameTime=100;
    
    [SerializeField] UI_Number _unidad;
    [SerializeField] UI_Number _decena;
    [SerializeField] UI_Number _centena;

    [SerializeField] AnimationCurve _animationCurve;

    int _time;
    int _lastTime=0;
    bool _paused = false;

    public Action<int> onAddTime;

    void Awake() {
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 
    }

    void Start(){
        GameManager.instance.onChangeState += OnChangeGameState;        
    }

    void OnDisable() {
        GameManager.instance.onChangeState -= OnChangeGameState;        
    }

    void OnChangeGameState(GameState state){
        switch (state){
            case GameState.NotStarted:
                StopAllCoroutines();
                Set3DTimer(_gameTime);
                break;
            case GameState.InGame:
                if (_paused)
                    _paused=false;
                else
                    StartGame();
                break;
            case GameState.Pause:
                _paused = true;
                break;
            case GameState.GameOver:
                StopAllCoroutines();
                Set3DTimer(0);
                break;
            default:
                break;
        }
    }

    void StartGame(){
        _time = _gameTime;
        StartCoroutine(SetTimer());
    }

    IEnumerator SetTimer(){
        while (_time>0){
            RotateTimer();
            yield return new WaitForSeconds(1);
            _time--;
        }
        GameManager.instance.GameOver();
    }

    void RotateTimer(){
        string formatedScore = _time.ToString().PadLeft(3, '0');
        _unidad.RotateAtNumber(int.Parse(formatedScore[2].ToString()), _animationCurve);
        _decena.RotateAtNumber(int.Parse(formatedScore[1].ToString()), _animationCurve);
        _centena.RotateAtNumber(int.Parse(formatedScore[0].ToString()), _animationCurve);
    }
    
    void Set3DTimer(int time){
        string formatedScore = time.ToString().PadLeft(3, '0');
        _unidad.RotateAtNumber(int.Parse(formatedScore[2].ToString()), _animationCurve);
        _decena.RotateAtNumber(int.Parse(formatedScore[1].ToString()), _animationCurve);
        _centena.RotateAtNumber(int.Parse(formatedScore[0].ToString()), _animationCurve);
    }

    public void AddTime(int addTime){
        _time+=addTime;
        RotateTimer();
        if (onAddTime != null)
            onAddTime(addTime);
    }
}
