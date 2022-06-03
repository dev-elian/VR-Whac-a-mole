using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThorHammerAnimation : MonoBehaviour, IHammerActions
{
    [SerializeField] Animator _anim;
    [SerializeField] ElectricParticlesSound _particles;
    [SerializeField] BoxCollider _gripCollider;
    bool _finished = false;


    void Start() {
        DropHammer();
    }

    public void GetHammer(){
        _particles.SetWorthy(true);
        _anim.SetBool("_selecting", true);
    }

    public void DropHammer(){
        if (!_finished){
            _particles.SetWorthy(false);
            _anim.SetBool("_selecting", false);            
        }
    }

    void LateUpdate() {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("EndAnimation") && !_finished){
            _finished = true;
            Invoke("MakeGrip", 3f);
        }
    }

    void MakeGrip(){
        _gripCollider.enabled = true;
        Destroy(_anim);
        Destroy(_particles.gameObject);
        Destroy(gameObject);
    }
}
