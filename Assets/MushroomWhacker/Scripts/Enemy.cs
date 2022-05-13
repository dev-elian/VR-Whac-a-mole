using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool _isKicked;
    public bool IsKicked {
        get {return _isKicked;}
    }

    [HideInInspector]
    public EnemiesInstancer instancer;
    [HideInInspector]
    public int hole;

    EnemyScriptable _enemyData;
    public Action<EnemyScriptable> onInstanciateEnemy;
    public Action onPunch;

    void Start(){
        _isKicked = false;
    }

    public IEnumerator IncreaseScore(){
        yield return new WaitForSeconds(0.1f);
        if (onPunch != null){
            onPunch.Invoke();
        }
        _isKicked = true;
        //add points
        yield return new WaitForSeconds(0.5f);
        RemoveHole();
    }

    public IEnumerator Destroy(){
        yield return new WaitForSeconds(0.1f);
        _isKicked = true;
        RemoveHole();
    }

    public void RemoveHole(){
        if (instancer != null)
            instancer.RemoveEnemy(hole);
        Destroy(gameObject);
    }

    public void SetEnemyFeatures(EnemyScriptable data){
        _enemyData = data;
        if (onInstanciateEnemy != null)
            onInstanciateEnemy.Invoke(data);
    }
}
