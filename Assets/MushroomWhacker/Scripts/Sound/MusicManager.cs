using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource _music;
    
    void Start(){
        GameManager.instance.onChangeState += ChangeState;
    }

    void OnDisable(){
        GameManager.instance.onChangeState -= ChangeState;
    }

    void ChangeState(GameState state){
        switch (state){
            case GameState.NotStarted:
                _music.Stop();
                break;
            case GameState.InGame:
                _music.Play();
                break;
            case GameState.Pause:
                _music.Pause();
                break;
            case GameState.GameOver:
                _music.Stop();
                break;
            default:
                _music.Stop();
                break;
        }
    }
}
