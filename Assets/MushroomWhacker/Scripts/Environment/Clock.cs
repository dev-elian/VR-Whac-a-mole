using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] Transform _hour;
    [SerializeField] Transform _min;
    [SerializeField] Transform _sec;

    IEnumerator Start(){
        while (true){
            DateTime actualTime =  DateTime.Now;
            _sec.rotation = Quaternion.Euler(0,0,actualTime.Second*6);
            _min.rotation = Quaternion.Euler(0,0,actualTime.Minute*6);
            float x = ((actualTime.Hour%12)*30)+ actualTime.Minute/60;
            _hour.rotation = Quaternion.Euler(0,0,x);
            yield return new WaitForSeconds(1);
        }
    }
}
