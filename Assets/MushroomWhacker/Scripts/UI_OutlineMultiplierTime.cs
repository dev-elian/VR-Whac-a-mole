using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OutlineMultiplierTime : MonoBehaviour
{
    [SerializeField] Image _image;

    void Update(){
        _image.fillAmount = ScoreManager.instance.timeSpecialMultiplier;
    }
}
