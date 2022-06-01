using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomMaterial : MonoBehaviour
{
    [SerializeField] Enemy _enemy;

    void Awake() {
        _enemy.onInstanciateEnemy += SetObjects;
    }

    void OnDisable() {
        Debug.Log(_enemy);
        _enemy.onInstanciateEnemy -= SetObjects;
    }

    void SetObjects(EnemyScriptable data){
        Instantiate(data.mesh, transform.position, Quaternion.identity, transform);
        Instantiate(data.detectable, transform.position, Quaternion.identity, transform);
    }
}
