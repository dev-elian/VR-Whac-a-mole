using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruction : MonoBehaviour
{
    [SerializeField] float _timeToDestroy=0.5f;
    IEnumerator Start(){
        yield return new WaitForSeconds(_timeToDestroy);
        Destroy(gameObject);
    }
}
