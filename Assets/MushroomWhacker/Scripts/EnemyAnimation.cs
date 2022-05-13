using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Enemy _enemy;
    AnimationCurve _verticalPositionCurve;
    [SerializeField] float _finalVerticalPosition=1;
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
        _verticalPositionCurve = new AnimationCurve();
        foreach (var item in enemy.verticalPositionCurve.keys){
            _verticalPositionCurve.AddKey(
                new Keyframe(item.time, item.value*_finalVerticalPosition, item.inTangent, item.outTangent, item.inWeight, item.outWeight)
            );
        }
        _moving = true;
    }

    void Update() {
        if (_moving){
            _accumulatedTime+=Time.deltaTime;
            transform.position= new Vector3(_initPosition.x, _initPosition.y+_verticalPositionCurve.Evaluate(_accumulatedTime), _initPosition.z);
            if (_accumulatedTime>_verticalPositionCurve.keys[_verticalPositionCurve.length-1].time)
                Remove();
        }
    }

    void Punch(){
        if (_accumulatedTime<_verticalPositionCurve.keys[2].time)
            _accumulatedTime =_verticalPositionCurve.keys[2].time;
    }

    void Remove(){
        Debug.Log("remove");
        GetComponent<Enemy>().RemoveHole();
        StartCoroutine(_enemy.Destroy());
    }

}
