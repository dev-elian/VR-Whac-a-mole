using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineDetectable : MonoBehaviour
{
    [SerializeField] int _penaltyPointsPerStroke=5;
    void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.Weapon)
            ScoreManager.instance.DecreaseScore(_penaltyPointsPerStroke);
    }
}
