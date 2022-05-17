using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType{
    Normal = 0,
    TimeIncreaser = 1,
    ScoreMultiplier = 2
}

[CreateAssetMenu(fileName = "NewType", menuName = "Enemy")]
public class EnemyScriptable: ScriptableObject{
    public AnimationCurve verticalPositionCurve;
    public Material materialForMesh;
    public int pointsByPunch;
    public EnemyType type;
}
