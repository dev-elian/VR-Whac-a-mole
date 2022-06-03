using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricParticlesSound : MonoBehaviour
{
    [SerializeField] List<AudioClip> _clips;
    [SerializeField] ParticleSystem _particles;
    [SerializeField] AudioSource _audio;
    int currentNumberOfParticles=0;
    float _restTime=0;
    bool _emit = false;

    void Update(){
        if (_emit){
            if (_restTime<0){
                if(_particles.particleCount!=currentNumberOfParticles){
                    _audio.clip = _clips[Random.Range(0, _clips.Count)];
                    _restTime = _audio.clip.length;
                    _audio.Play();
                }
                _audio.volume = Random.Range(0.1f,1);
                currentNumberOfParticles  = _particles.particleCount;            
            }
            _restTime-=Time.deltaTime;
        }
        Debug.Log("P"+_particles.isPlaying);
    }

    public void SetWorthy(bool emit){
        _emit = emit;
        if (emit)
            _particles.Play();
        else
            _particles.Stop();
    }
}
