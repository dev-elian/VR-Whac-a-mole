using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance { get; private set; }        

    [SerializeField] int _streakForComboMultiplier=5;
    [SerializeField] int _maxComboPossible=8;
    [SerializeField] int _initSpecialTimeMultiplier=5;

    int _combo=1;
    int _score=0;
    int _streak=0;
    bool _pause = false;
    int _specialMultiplier=1;
    float _specialTimeMultiplier=0;
    bool _multiplierActive = false;

    public int score {get=>_score;}
    public int maxComboPossible {get => _maxComboPossible;}
    public int streak {get=>_streak;}
    public int combo {get => _combo;
                      private set {_combo = value<=maxComboPossible?value:_maxComboPossible;}}
    
    public float timeSpecialMultiplier {get => (_specialTimeMultiplier>0?_specialTimeMultiplier/_initSpecialTimeMultiplier:0); set => _specialTimeMultiplier = value;}

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
        _streak = 0;
    }

    public void FailEnemy(){
        _streak = 0;
        ChangeStreak();
    }

    void ChangeStreak(){
        combo = (_streak != 0 ? _streak/_streakForComboMultiplier+1 : 1);
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
                    Timer.instance.AddTime(value);
                    break;
                case EnemyType.ScoreMultiplier:
                    StopAllCoroutines();
                    StartCoroutine(SetSpecialEnemyMultiplier());
                    break;
                case EnemyType.ScoreDecreaser:
                    DecreaseScore(value);
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
        int punchValue = (value*(combo*_specialMultiplier));
        _score += punchValue;
        if (_score<=0)
            _score = 0;
        if (onChangeScore != null)
            onChangeScore(_score);
        if (onPunchScore != null)
            onPunchScore(punchValue);
        if (_score == 0)
            GameManager.instance.GameOver();
    }

    IEnumerator SetSpecialEnemyMultiplier(){
        _specialMultiplier=2;
        timeSpecialMultiplier = _initSpecialTimeMultiplier;
        if (onActiveSpecialEnemy != null)
            onActiveSpecialEnemy(true);
        _multiplierActive = true;
        yield return new WaitForSeconds(_initSpecialTimeMultiplier);
        if (onActiveSpecialEnemy != null)
            onActiveSpecialEnemy(false);
        _specialMultiplier=1;
        ChangeStreak();
    }

    void Update() {
        if (_multiplierActive){
            _specialTimeMultiplier-=Time.deltaTime;
            if (_specialTimeMultiplier<0)
                _multiplierActive = false;
        }
    }

    public void SaveScore(){
        if (PlayerPrefs.GetInt("_maxScore",0)<_score){
            PlayerPrefs.SetInt("_maxScore", _score);
            PlayerPrefs.Save();
        }
        AchievmentsManager.instance.VerifyAchievments();
    }
}
