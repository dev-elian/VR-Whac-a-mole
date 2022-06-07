using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerDetectable : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        GetComponentInParent<Hammer>().HammerCrash(other);
    }
}
