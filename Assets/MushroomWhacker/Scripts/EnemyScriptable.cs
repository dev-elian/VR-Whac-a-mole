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
    public EnemyType type;
    public AnimationCurve verticalPositionCurve;
    public bool breakStreak;
    public GameObject particles;

    public GameObject mesh;
    public GameObject detectable;

    [Header("Points Per Punch | Time To Add")]
    public int value;
}
