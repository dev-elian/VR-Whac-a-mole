using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineDetectable : MonoBehaviour
{
    [SerializeField] AudioSource _sound;
    [SerializeField] int _penaltyPointsPerStroke=5;
    void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.Weapon){
            ScoreManager.instance.DecreaseScore(_penaltyPointsPerStroke);
            _sound.Play();
        }
    }
}
