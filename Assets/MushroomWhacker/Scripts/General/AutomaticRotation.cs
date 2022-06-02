using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticRotation : MonoBehaviour
{
    [SerializeField] Vector3 _rotationVector;
    [SerializeField] Space _space;

    bool _rotating = false;

    void OnEnable() {
        GetComponent<HammerSelector>().OnSelecting+= EnabledRotation;
    }

    void OnDisable() {
        GetComponent<HammerSelector>().OnSelecting-= EnabledRotation;
    }

    void EnabledRotation(bool selecting){
        _rotating = selecting;
    }

    void Update(){
        if (_rotating)
            transform.Rotate(_rotationVector*Time.deltaTime, _space);
    }
}
