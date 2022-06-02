using System;
using UnityEngine;

public enum ScoreChange{
    Increase = 0,
    Decrease =1
}
public class UI_Score : MonoBehaviour
{
    [SerializeField] UI_Number _unidad;
    [SerializeField] UI_Number _decena;
    [SerializeField] UI_Number _centena;
    [SerializeField] UI_Number _uDeMil;

    [SerializeField] AnimationCurve _animationCurve;
    public Action onBadPunch;
    int _lastScore=0;

    void Start(){
        GameManager.instance.onChangeState += OnChangeGameState;        
        ScoreManager.instance.onChangeScore += Rotate3DScore;
    }

    void OnDisable() {
        GameManager.instance.onChangeState -= OnChangeGameState;        
        ScoreManager.instance.onChangeScore -= Rotate3DScore;
    }

    void OnChangeGameState(GameState state){
        switch (state){
            case GameState.NotStarted:
                Reset3DScore();
                break;
            case GameState.GameOver:
                Reset3DScore();
                break;
            default:
                break;
        }
    }

    void Rotate3DScore(int score){
        if (_lastScore>score && onBadPunch !=null)
            onBadPunch();
        _lastScore=score;

        string formatedScore = score.ToString().PadLeft(4, '0');
        _unidad.RotateAtNumber(int.Parse(formatedScore[3].ToString()), _animationCurve);
        _decena.RotateAtNumber(int.Parse(formatedScore[2].ToString()), _animationCurve);
        _centena.RotateAtNumber(int.Parse(formatedScore[1].ToString()), _animationCurve);
        _uDeMil.RotateAtNumber(int.Parse(formatedScore[0].ToString()), _animationCurve);
    }

    void Reset3DScore(){
        _unidad.RotateAtNumber(0, _animationCurve);
        _decena.RotateAtNumber(0, _animationCurve);
        _centena.RotateAtNumber(0, _animationCurve);
        _uDeMil.RotateAtNumber(0, _animationCurve);
    }
}
