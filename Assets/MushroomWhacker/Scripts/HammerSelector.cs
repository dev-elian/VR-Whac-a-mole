using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HammerSelector : MonoBehaviour
{
    [SerializeField] float _transitionTime=1;
    [SerializeField] List<GameObject> _hammers;
    List<GameObject> _meshHammers;
    int idx=0;

    bool _selecting = false;
    bool _showing = false;

    public Action<bool> OnSelecting;

    void Awake() {
        _meshHammers = new List<GameObject>();
        foreach (GameObject obj in _hammers){
            Transform tmpHammer = Instantiate(obj, transform).transform;
            tmpHammer.GetComponent<BoxCollider>().enabled = false;
            tmpHammer.localPosition = Vector3.zero;
            tmpHammer.localRotation = Quaternion.identity;
            _meshHammers.Add(tmpHammer.gameObject);
            tmpHammer.gameObject.SetActive(false);
        }
    }

    void Start() {
        PlayerControllers.instance.onShowSelection += ShowHammers;
        PlayerControllers.instance.onSelect += MoveSelection;
        PlayerControllers.instance.onAccept += AcceptHammer;
    }

    void OnDisable() {
        PlayerControllers.instance.onShowSelection -= ShowHammers;
        PlayerControllers.instance.onSelect -= MoveSelection;
        PlayerControllers.instance.onAccept -= AcceptHammer;
    }

    void MoveSelection(Side side){
        if (_showing && _selecting){
            switch (side){
                case Side.Left:
                    idx = (idx-1<0?_hammers.Count-1:idx-1);
                    _selecting = false;
                    DisableAll();
                    Invoke("EnableSelection", _transitionTime);
                    break;
                case Side.Right:
                    idx = (idx+1>=_hammers.Count?0:idx+1);
                    _selecting = false;
                    DisableAll();
                    Invoke("EnableSelection", _transitionTime);
                    break;
                default:
                    break;
            }
            _meshHammers[idx].SetActive(true);
        }
    }

    void EnableSelection(){
        _selecting = true;
    }

    void ShowHammers(bool show){
        _selecting = show;
        _showing = show;
        if (OnSelecting != null)
            OnSelecting(true);
        DisableAll();
        _meshHammers[idx].SetActive(show);
    }

    void AcceptHammer(){
        if (_selecting){
            _selecting = false;
            _showing = false;
            DisableAll();
            if (OnSelecting != null)
                OnSelecting(false);
            Instantiate(_hammers[idx], transform.position, Quaternion.identity);
        }
    }

    void DisableAll(){
        for (int i = 0; i < _meshHammers.Count; i++){
            _meshHammers[i].SetActive(false);
        }
    }

    public void AddHammer(GameObject hammer){
        _hammers.Add(hammer);
        Transform tmpHammer = Instantiate(hammer, transform).transform;
        tmpHammer.GetComponent<BoxCollider>().enabled = false;
        tmpHammer.localPosition = Vector3.zero;
        tmpHammer.localRotation = Quaternion.identity;
        _meshHammers.Add(tmpHammer.gameObject);
        tmpHammer.gameObject.SetActive(false);
    }
}
