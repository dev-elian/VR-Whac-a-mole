using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectable : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == Tags.Weapon){
            if (other.GetComponent<HammerReloader>().CanCollide)
                StartCoroutine(GetComponentInParent<Enemy>().Destroy());
        }
    }
}