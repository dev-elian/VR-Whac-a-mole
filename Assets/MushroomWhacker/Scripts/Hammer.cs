using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public bool _canCollide;
    public bool CanCollide {
        get {return _canCollide;}
    }

    [SerializeField] MeshRenderer _mesh;
    [SerializeField] Material _enabled;
    [SerializeField] Material _disabled;

    void Start(){
        _mesh.material = _enabled;
        _canCollide=true; 
    }
    
    public void HammerCrash(Collider other) {
        if (GameManager.instance.gameState == GameState.InGame){
            switch (other.gameObject.tag){
                case Tags.Reloader:
                    _mesh.material = _enabled;
                    _canCollide=true;
                    break;
                case Tags.Enemy:
                    _mesh.material = _disabled;
                    _canCollide=false;
                    StartCoroutine(other.GetComponentInParent<Enemy>().PunchEnemy());
                    break;
                case Tags.Machine:
                    _mesh.material = _disabled;
                    _canCollide=false;
                    break;
                default:
                    break;
            }
        }
    }

    IEnumerator ChangeCollide(){
        yield return new WaitForSeconds(0.05f);
        _canCollide=false;
    }
}
