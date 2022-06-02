using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierSound : MonoBehaviour
{
    [SerializeField] AudioSource _sound;
    
    void Start(){
        ScoreManager.instance.onActiveSpecialEnemy += PlaySound;
    }
    void OnDisable(){
        ScoreManager.instance.onActiveSpecialEnemy -= PlaySound;
    }

    void PlaySound(bool active){
        if (active)
            _sound.Play();
    }
}
