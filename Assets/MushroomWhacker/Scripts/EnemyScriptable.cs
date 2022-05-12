using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewType", menuName = "Enemy")]
public class EnemyScriptable: ScriptableObject{
    public AnimationCurve positionAndTimeCurve;
    public GameObject modelForMesh;
    public float pointsByPunch;
}
