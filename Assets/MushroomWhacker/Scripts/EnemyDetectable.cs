using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectable : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == Tags.Weapon){
            if (other.GetComponentInParent<Hammer>().CanCollide){
                Debug.Log(123);
                StartCoroutine(GetComponentInParent<Enemy>().IncreaseScore());
            }
        }
    }
}