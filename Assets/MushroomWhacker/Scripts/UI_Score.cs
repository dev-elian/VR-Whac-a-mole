using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Score : MonoBehaviour
{
    [SerializeField] Transform _unidad;
    [SerializeField] Transform _decena;
    [SerializeField] Transform _centena;
    [SerializeField] Transform _uDeMil;
    void Start(){
        GameManager.instance.onChangeState += OnChangeGameState;        
        ScoreManager.instance.onChangeScore += Set3DScore;
    }

    void OnDisable() {
        GameManager.instance.onChangeState -= OnChangeGameState;        
        ScoreManager.instance.onChangeScore -= Set3DScore;
    }

    void OnChangeGameState(GameState state){
        switch (state){
            case GameState.NotStarted:
                break;
            case GameState.InGame:
                break;
            case GameState.GameOver:
                break;
            default:
                break;
        }
    }

    void Set3DScore(int score){
        string formatedScore = score.ToString().PadLeft(4, '0');
        Debug.Log(formatedScore);
        _unidad.rotation = Quaternion.Euler(float.Parse(formatedScore[3].ToString())*36f,_unidad.rotation.y,_unidad.rotation.z);
        _decena.rotation = Quaternion.Euler(float.Parse(formatedScore[2].ToString())*36f,_unidad.rotation.y,_unidad.rotation.z);
        _centena.rotation = Quaternion.Euler(float.Parse(formatedScore[1].ToString())*36f,_unidad.rotation.y,_unidad.rotation.z);
        _uDeMil.rotation = Quaternion.Euler(float.Parse(formatedScore[0].ToString())*36f,_unidad.rotation.y,_unidad.rotation.z);
    }
}
