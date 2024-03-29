using UnityEngine;

public class UI_Number : MonoBehaviour{
    
    [SerializeField] float _timeToSetNumber=0.7f;
    [SerializeField] float _timeToReset=1f;
    float _timeToRotate=0;

    public float _accumulatedTime=0;
    public float _baseAngle =0;
    public float _angleToReach=0;
    public float _lastAngle = 0;
 
    public AnimationCurve _curve;
    public int _lastNumber = 0;
 
    public bool _rotating = false;

    void Update(){
        if (_rotating){
            _accumulatedTime+=Time.deltaTime;
            transform.localRotation = Quaternion.Euler(_baseAngle+ _curve.Evaluate(_accumulatedTime)*_angleToReach,0,0);
            if (_accumulatedTime>=_timeToRotate){
                SetFinalRotation();
            }
        }
    }

    public void RotateAtNumber(int number, AnimationCurve curve){
        if (_rotating)
            SetFinalRotation();
        _angleToReach = FindShortestWay(number);
        _baseAngle = _lastNumber*36;
        _lastNumber = number;
        _accumulatedTime=0;
        if (GameManager.instance.gameState == GameState.GameOver){
            float x = _timeToReset*(Mathf.Abs(_angleToReach)/36);
            _curve = SetCurve(curve, x);
            _timeToRotate = x;
        }else{
            _curve = SetCurve(curve, _timeToSetNumber);
            _timeToRotate = _timeToSetNumber;
        }
        _rotating = true;
    }

    void SetFinalRotation(){
        _rotating = false;
        _lastAngle+=_angleToReach%360;
        transform.localRotation = Quaternion.Euler(_baseAngle+_angleToReach,0,0);
    }

    int FindShortestWay(int number){
        int stepsToRight = 0;
        int stepsToLeft = 0; 
        for (int i = _lastNumber; i < _lastNumber+10; i++){
            int actualDigit = int.Parse(i.ToString()[i.ToString().Length-1].ToString());
            if (number==actualDigit)
                break;
            stepsToRight++;
        }
        for (int i = _lastNumber+10; i > _lastNumber; i--){
            int actualDigit = int.Parse(i.ToString()[i.ToString().Length-1].ToString());
            if (number==actualDigit)
                break;
            stepsToLeft++;
        }
        if (stepsToLeft<stepsToRight)
            return stepsToLeft*-36;
        else
            return stepsToRight*36;
    }

    AnimationCurve SetCurve(AnimationCurve curve, float timeFactor){
        AnimationCurve newCurve = new AnimationCurve();
        foreach (var item in curve.keys){
            newCurve.AddKey(
                new Keyframe(item.time*timeFactor, item.value)
            );
        }
        return newCurve;
    }
}
