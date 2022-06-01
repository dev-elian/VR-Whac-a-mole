using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineButton : MonoBehaviour
{
    public Action<bool> onChangeButtonState;

    void Start() {
        GameManager.instance.onChangeState+=DesactivateButton;
    }

    void OnDisable() {
        GameManager.instance.onChangeState-=DesactivateButton;
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Tags.Hand))
            ActivateButton();
    }

    void ActivateButton(){
        GetComponent<BoxCollider>().enabled = false;
        GameManager.instance.StartGame();
        if (onChangeButtonState != null){
            onChangeButtonState(true);
        }
    }

    void DesactivateButton(GameState state){
        if (state==GameState.GameOver){
            GetComponent<BoxCollider>().enabled = true;
            if (onChangeButtonState != null){
                onChangeButtonState(false);
            } 
        }
    }
}
