using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TimePoints : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI _tmpro;

    void Start() {
        Timer.instance.onAddTime += ShowNumber;
        _tmpro.text = "";
    }

    void OnDisable() {
        Timer.instance.onAddTime -= ShowNumber;
    }

    void ShowNumber(int time){
        if (time != 0){
            StopAllCoroutines();
            _tmpro.text = (time>0?"+":"")+time;
            StartCoroutine(ShowNumber());
        }
    }

    IEnumerator ShowNumber(){
        yield return new WaitForSeconds(1);
        _tmpro.text = "";
    }
}
