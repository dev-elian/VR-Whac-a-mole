using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType{
    Normal = 0,
    TimeIncreaser = 1,
    ScoreMultiplier = 2,
    ScoreDecreaser = 3
}

[CreateAssetMenu(fileName = "NewType", menuName = "Enemy")]
public class EnemyScriptable: ScriptableObject{
    public AnimationCurve verticalPositionCurve;
    public Material materialForMesh;
    [Header("Points Per Punch | Time To Add")]
    public int value;
    public EnemyType type;
}
