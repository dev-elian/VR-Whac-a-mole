using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerReloader : MonoBehaviour
{
    public bool _canCollide;
    public bool CanCollide {
        get {return _canCollide;}
    }

    [SerializeField] MeshRenderer _mesh;
    [SerializeField] Material _enabled;
    [SerializeField] Material _disabled;


    void Start()
    {
        _mesh.material = _enabled;
        _canCollide=true;
    }
    void OnTriggerEnter(Collider other) {
        switch (other.gameObject.tag)
        {
            case Tags.Reloader:
                _mesh.material = _enabled;
                _canCollide=true;
                break;
            case Tags.Enemy:
                if (!other.GetComponentInParent<Enemy>().IsKicked)
                {
                    _mesh.material = _disabled;
                    StartCoroutine(ChangeCollide());
                }
                break;
            case Tags.Machine:
                _mesh.material = _disabled;
                StartCoroutine(ChangeCollide());
                break;
            default:
                break;
        }
    }

    IEnumerator ChangeCollide(){
        yield return new WaitForSeconds(0.1f);
        _canCollide=false;
    }
}
