using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomMaterial : MonoBehaviour
{
    Enemy _enemy;
    [SerializeField] SkinnedMeshRenderer _renderer;

    void Awake() {
        _enemy = GetComponentInParent<Enemy>();
    }

    void OnEnable() {
        _enemy.onInstanciateEnemy += SetMaterial;
    }

    void OnDisable() {
        _enemy.onInstanciateEnemy -= SetMaterial;
    }

    void SetMaterial(EnemyScriptable data){
        _renderer.material = data.materialForMesh;
    }
}
