using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int _score=0;//
    public int score {get=>_score;}

    public static ScoreManager instance { get; private set; }

    public Action<int> onChangeScore;

    void Awake() {
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 
    }
    
    void Start() {
        GameManager.instance.onChangeState += OnChangeGameState;        
    }

    void OnDisable() {
        GameManager.instance.onChangeState -= OnChangeGameState;        
    }

    void OnChangeGameState(GameState state){
        switch (state){
            case GameState.NotStarted:
                RestartScore();
                break;
            case GameState.InGame:
                RestartScore();
                break;
            case GameState.GameOver:
                SaveScore();
                break;
            default:
                break;
        }
    }

    void RestartScore(){
        _score = 0;
    }

    public void IncreaseScore(int value){
        if (GameManager.instance.gameState == GameState.InGame){
            _score += value;
            if (onChangeScore != null)
                onChangeScore(value);
        }
    }

    public void DecreaseScore(int value){
        if (GameManager.instance.gameState == GameState.InGame){
            _score -= value;
            if (onChangeScore != null)
                onChangeScore(value);
            if (_score<=0){
                _score = 0;
                GameManager.instance.GameOver();
            }
        }
    }

    public void SaveScore(){
        if (PlayerPrefs.GetInt("maxScore",0)<_score){
            PlayerPrefs.SetInt("maxScore", _score);
            PlayerPrefs.Save();
        }
    }
}
