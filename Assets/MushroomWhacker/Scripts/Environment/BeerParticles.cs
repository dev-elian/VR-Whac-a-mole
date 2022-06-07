using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerParticles : MonoBehaviour
{
    [SerializeField] ParticleSystem _particles;
    [SerializeField] float _numberParticles=15;
    [SerializeField] float _minAngle=60;

    void FixedUpdate() {
        var emission = _particles.emission;
        Vector3 rotation = transform.rotation.eulerAngles;
        if (VerifyAngle(rotation.x) || VerifyAngle(rotation.z))
            emission.rateOverTime = _numberParticles;
        else
            emission.rateOverTime = 0;
    }

    bool VerifyAngle(float angle){
        return (angle>_minAngle && angle<360-_minAngle);
    }



    // float Normalize(float val, float valmin, float valmax, float newMin=0, float newMax=1) 
    // {
    //     if (val<valmin)
    //         return newMin;
    //     if (val>valmax)
    //         return newMax;
    //     return (((val - valmin) / (valmax - valmin)) * (newMax - newMin)) + newMin;
    // }
}
