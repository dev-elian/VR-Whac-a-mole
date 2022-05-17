using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool _isKicked;
    [HideInInspector]
    public EnemiesInstancer instancer {get; private set;}
    [HideInInspector]
    public int hole {get; private set;}

    EnemyScriptable _enemyData;
    public Action<EnemyScriptable> onInstanciateEnemy;
    public Action onPunch;

    public void SetInitValues(EnemiesInstancer instancer, int hole, EnemyScriptable data){
        this.instancer = instancer;
        this.hole = hole;
        _enemyData = data;
        if (onInstanciateEnemy != null)
            onInstanciateEnemy.Invoke(data);
    }

    void Start(){
        _isKicked = false;
    }

    public IEnumerator PunchEnemy(){
        if (!_isKicked){
            _isKicked = true;
            yield return new WaitForSeconds(0.1f);
            if (onPunch != null){
                onPunch.Invoke();
            }
            ScoreManager.instance.IncreaseScore(_enemyData.value, _enemyData.type);
            yield return new WaitForSeconds(0.5f);
            ClearHole();
        }
    }

    public IEnumerator Destroy(){
        yield return new WaitForSeconds(0.1f);
        ScoreManager.instance.FailEnemy();
        _isKicked = true;
        ClearHole();
    }

    public void ClearHole(){
        if (instancer != null)
            instancer.RemoveEnemy(hole);
        Destroy(gameObject);
    }
}
