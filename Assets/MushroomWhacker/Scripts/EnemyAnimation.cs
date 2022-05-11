using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] float _timeVisible=1;
    Animator _anim;
    void Awake() {
        _anim = transform.Find("Anim").GetComponent<Animator>();
    }

    IEnumerator Start(){
        yield return new WaitForSeconds(_timeVisible);
        _anim.SetBool("delete", true);
        GetComponent<Enemy>().RemoveHole();
        yield return new WaitForSeconds(1);
        Destroy(transform.gameObject);
    }
}
