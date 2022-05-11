using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool _isKicked;
    public bool IsKicked {
        get {return _isKicked;}
    }

    public EnemiesInstancer instancer;
    public int hole;

    void Start(){
        _isKicked = false;
    }

    public IEnumerator Destroy(){
        yield return new WaitForSeconds(0.1f);
        _isKicked = true;
        RemoveHole();
    }

    public void RemoveHole(){
        if (instancer != null)
            instancer.RemoveEnemy(hole);
    }
}
