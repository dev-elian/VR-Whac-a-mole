using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Achievment{
    public UnityEvent ev;
    public int scoreToReach;
    public bool invoked;
}

public class AchievmentsManager : MonoBehaviour
{

    public static AchievmentsManager instance;
    [SerializeField] List<Achievment> achievments;
    
    void Awake() {
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 
    }    

    IEnumerator Start() {
        yield return new WaitForSeconds(2);
        VerifyAchievments();
    }

    public void VerifyAchievments(){
        int maxScore = PlayerPrefs.GetInt("_maxScore");
        foreach (Achievment ach in achievments){
            if (maxScore>ach.scoreToReach && !ach.invoked){
                ach.invoked = true;
                ach.ev.Invoke();
            }
        }
    }
}
