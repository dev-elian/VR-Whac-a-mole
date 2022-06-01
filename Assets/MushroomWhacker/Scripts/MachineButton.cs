using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineButton : MonoBehaviour
{
    [SerializeField] float _timeTransition=1;
    [SerializeField] Vector3 _initPosition;
    [SerializeField] Vector3 _finalPosition;


    //transición a botón e inicion de juego
    void OnTriggerEnter(Collider other) {
        // Interpolation.Interpolate(_timeTransition, 0, 1,)
        GameManager.instance.StartGame();
    }
}
