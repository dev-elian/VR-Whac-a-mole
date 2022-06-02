using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TextPoints : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI _tmpro;

    void Start() {
        ScoreManager.instance.onPunchScore += ShowNumber;
        _tmpro.text = "";
    }

    void OnDisable() {
        ScoreManager.instance.onPunchScore -= ShowNumber;
    }

    void ShowNumber(int score){
        if (score != 0){
            StopAllCoroutines();
            _tmpro.text = (score>0?"+":"")+score;
            StartCoroutine(ShowNumber());
        }
    }

    IEnumerator ShowNumber(){
        yield return new WaitForSeconds(1);
        _tmpro.text = "";
    }
}
