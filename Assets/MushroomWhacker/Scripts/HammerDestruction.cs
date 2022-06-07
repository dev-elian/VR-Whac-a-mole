using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerDestruction : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag(Tags.RoomCollisions))
            Destroy(gameObject);
    }
}
