using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntermitent : MonoBehaviour
{
    [SerializeField] float _interval;
    MeshRenderer _rend;

    IEnumerator Start(){
        _rend = GetComponent<MeshRenderer>();
        while (true){
            _rend.material.SetColor("_EmissionColor", Color.black*0);
            yield return new WaitForSeconds(_interval);
            _rend.material.SetColor("_EmissionColor", Color.white*3);
            yield return new WaitForSeconds(_interval);
            
        }
    }
}
