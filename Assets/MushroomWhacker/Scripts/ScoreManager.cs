using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance { get; private set; }
    [SerializeField] int _streakForComboMultiplier=5;

    bool _pause = false;

    int _score=0;
    public int score {get=>_score;}

    [SerializeField] int _maxComboPossible=8;
    public int maxComboPossible {get => _maxComboPossible;}

    int _streak=0;
    public int streak {get=>_streak;}

    int _combo;
    public int combo {get => _combo;
                      private set {_combo = value<=maxComboPossible?value:_maxComboPossible;}
                    }

    [SerializeField] int _timeSpecialMultiplier=5;
    int _specialMultiplier=1;

    public Action<int> onChangeScore;
    public Action<int> onPunchScore;
    public Action<int, int> onChangeStreak;
    public Action<bool> onActiveSpecialEnemy;

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
                if (_pause)
                    _pause = false;
                else
                    RestartScore();
                break;
            case GameState.Pause:
                _pause = true;
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
        _streak=0;
    }

    public void FailEnemy(){
        _streak=0;
        ChangeStreak();
    }

    void ChangeStreak(){
        combo = (_streak != 0 ? _streak/_streakForComboMultiplier+1 : 0);
        int streakRest = (_streak != 0 ? _streak%_streakForComboMultiplier : 0);
        if (onChangeStreak != null)
            onChangeStreak(combo*_specialMultiplier, streakRest);
    }

    public void IncreaseScore(int value, EnemyType enemyType){
        if (GameManager.instance.gameState == GameState.InGame){
            _streak++;
            switch (enemyType){
                case EnemyType.Normal:
                    ChangeScore(value);
                    break;
                case EnemyType.TimeIncreaser:
                    UI_Timer.instance.AddTime();
                    break;
                case EnemyType.ScoreMultiplier:
                    StopAllCoroutines();
                    StartCoroutine(SetSpecialEnemyMultiplier());
                    break;
                default:
                    break;
            }
            ChangeStreak();
        }
    }

    public void DecreaseScore(int value){
        if (GameManager.instance.gameState == GameState.InGame){
            _streak = 0;
            ChangeScore(value*-1);
            ChangeStreak();
        }
    }

    void ChangeScore(int value){
        _score += value;
        if (_score<=0)
            _score = 0;
        if (onChangeScore != null)
            onChangeScore(_score);
        if (onPunchScore != null)
            onPunchScore(value);
        if (_score == 0)
            GameManager.instance.GameOver();
    }

    IEnumerator SetSpecialEnemyMultiplier(){
        _specialMultiplier++;
        if (onActiveSpecialEnemy != null)
            onActiveSpecialEnemy(true);
        yield return new WaitForSeconds(_timeSpecialMultiplier);
        if (onActiveSpecialEnemy != null)
            onActiveSpecialEnemy(false);
        _specialMultiplier = 1;
        ChangeStreak();
    }

    public void SaveScore(){
        if (PlayerPrefs.GetInt("maxScore",0)<_score){
            PlayerPrefs.SetInt("maxScore", _score);
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.GetInt("maxStreak",0)<_streak){
            PlayerPrefs.SetInt("maxStreak", _streak);
            PlayerPrefs.Save();
        }
    }
}
