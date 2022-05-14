using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{
    NotStarted = 0,
    InGame = 1,
    Pause = 2,
    GameOver = 3
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public GameState gameState;
    public Action<GameState> onChangeState;
    
    void Awake() {
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 
        gameState = GameState.NotStarted;
    }

    [ContextMenu("Start Game")]
    public void StartGame(){
        ChangeState(GameState.InGame);
    }

    [ContextMenu("Pause")]
    public void Pause(){
        ChangeState(GameState.Pause);
        Time.timeScale = 0;
    }

    [ContextMenu("Continue")]
    public void Continue(){
        ChangeState(GameState.InGame);
        Time.timeScale = 1;
    }

    [ContextMenu("Restart")]
    public void Restart(){
        ChangeState(GameState.NotStarted);
    }

    [ContextMenu("Game Over")]
    public void GameOver(){
        ChangeState(GameState.GameOver);
    }

    void ChangeState(GameState newState){
        gameState = newState;
        if(onChangeState != null)
            onChangeState(gameState);
    }
}
