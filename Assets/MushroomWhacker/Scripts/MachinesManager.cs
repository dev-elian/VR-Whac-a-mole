using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinesManager : MonoBehaviour
{
    [SerializeField] GameObject _mushroomMachine;
    [SerializeField] GameObject _thanosMachine;

    [SerializeField] GameObject _mushroomInstancer;
    [SerializeField] GameObject _thanosInstancer;

    [SerializeField] GameObject _transitionMachine;
    [SerializeField] Renderer _transitionMachineRend;
    bool _startTransition=false;

    [SerializeField] float _transitionTime=5;

    void Start(){
        _mushroomMachine.SetActive(true);
        _thanosMachine.SetActive(false);
        _transitionMachineRend.sharedMaterial.color = new Color(1,1,1,0);

        PlayerControllers.instance.onChangeMachine += ChangeMachine;
    }

    void OnDisable() {
        _transitionMachineRend.sharedMaterial.color = new Color(1,1,1,0);

        PlayerControllers.instance.onChangeMachine -= ChangeMachine;
    }

    public void ChangeMachine(){
        if (_mushroomMachine.activeInHierarchy){
            _mushroomMachine.SetActive(false);
            _thanosMachine.SetActive(true);   
            _thanosInstancer.SetActive(true);
            _mushroomInstancer.SetActive(false);
            return;        
        }else{
            _thanosInstancer.SetActive(false);
            _mushroomInstancer.SetActive(true);
            _mushroomMachine.SetActive(true);
            _thanosMachine.SetActive(false);
            
        }
    }

    public void SlowTransition(){
        _transitionMachine.SetActive(true);
        _transitionMachineRend.sharedMaterial.color = new Color(1,1,1,0);
        _startTransition = true;
        alpha = 0;
    }

    void Update() {
        if (_startTransition){
            if (_transitionMachineRend.sharedMaterial.color.a <1)
                UpdateSharedColor();
            else{
                ChangeMachine();
                _transitionMachine.SetActive(false);
                _startTransition = false;
            }
        }
    }

    float alpha = 0;
    void UpdateSharedColor(){
        alpha += (1/_transitionTime)*Time.deltaTime;
        _transitionMachineRend.sharedMaterial.color = new Color(1,1,1,alpha);
    }
}
