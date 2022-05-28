using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerDetectable : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        Debug.Log("XD"+other.gameObject.name);
        GetComponentInParent<Hammer>().HammerCrash(other);
    }
}
