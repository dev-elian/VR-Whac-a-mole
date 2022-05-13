using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewType", menuName = "Enemy")]
public class EnemyScriptable: ScriptableObject{
    public AnimationCurve verticalPositionCurve;
    public Material materialForMesh;
    public float pointsByPunch;
}
