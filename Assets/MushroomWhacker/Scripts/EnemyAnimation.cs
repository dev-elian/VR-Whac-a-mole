using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Enemy _enemy;
    AnimationCurve _positionAndTimeCurve;
    public float _accumulatedTime=0;
    Vector3 _initPosition;
    bool _moving = false;

    void Awake() {
        _enemy = GetComponent<Enemy>();
        _initPosition = transform.position;
    }

    void OnEnable() {
        _enemy.onInstanciateEnemy += SetCurve;
        _enemy.onPunch += Punch;
    }

    void OnDisable() {
        _enemy.onInstanciateEnemy -= SetCurve;
        _enemy.onPunch -= Punch;
    }

    void SetCurve(EnemyScriptable enemy){
        _positionAndTimeCurve = enemy.positionAndTimeCurve;
        _moving = true;
    }

    void Update() {
        if (_moving){
            _accumulatedTime+=Time.deltaTime;
            transform.position= new Vector3(_initPosition.x, _initPosition.y+_positionAndTimeCurve.Evaluate(_accumulatedTime), _initPosition.z);
            if (_accumulatedTime>_positionAndTimeCurve.keys[_positionAndTimeCurve.length-1].time)
                Remove();
        }
    }

    void Punch(){
        if (_accumulatedTime<_positionAndTimeCurve.keys[2].time)
            _accumulatedTime =_positionAndTimeCurve.keys[2].time;
    }

    void Remove(){
        GetComponent<Enemy>().RemoveHole();
        StartCoroutine(_enemy.Destroy());
    }

}
